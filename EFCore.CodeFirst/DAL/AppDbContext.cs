﻿using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    // DbContext Default -> Scoped 
    internal class AppDbContext : DbContext
    {
        // Dışardan bir Servis olabilir.
        // private readonly ITenantService tService;
        // Bu Barkod değeri dışardanda gelebilir.
        private readonly int Barcode;

        private DbConnection _connection;

        //public AppDbContext(int barcode)
        //{
        //    Barcode = barcode;
        //}


        public AppDbContext(DbConnection connection)
        {
            _connection = connection;
        }

        public AppDbContext()
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ProductFeature>? ProductFeatures { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // Veritabanına yansımasını engellemek için Migration içindeki Kodları temizlememiz gerekiyor 
        public DbSet<ProductEssential> ProductEssentials { get; set; }


        // BasePerson' ı miras alan Sınıfları tek bir sınıf halinde VT'de topluyor.
        // Ek olarak Discriminator alanı oluşturuyor . Managers ve Employees alanının farkını belirtmek için
        public DbSet<BasePerson> Persons { get; set; }

        // Keyless Entity
        public DbSet<ProductFull> ProductFulls { get; set; }

        // View Tablomuzu tanımlıyoruz
        public DbSet<ProductView> ProductView { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<ProductWithFeature> ProductWithFeatures { get; set; }

        // Keyless Entity olarak OnModelCreating üzerinde ayarlıyoruz.
        public DbSet<ProductCount> ProductCount { get; set; }

        // lambda ' dan sonra return FromExpression . 
        // Bir Function çağırmak istiyorsak bir metot ver.
        public IQueryable<ProductWithFeature> GetProductWithFeatures(int categoryId) => FromExpression<ProductWithFeature>(() => GetProductWithFeatures(categoryId));

        public int GetProductCountByCategoryId(int categoryId)
        {
            // buradaki amaç bu metodu EF Core sadece context üzerinden dönmemize izin vermiyor.
            throw new NotSupportedException("Bu metot EF Core tarafından çağrılmaktadır.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Sadece Console'da bu ayarı yapıyoruz. Migrationda ConStr okuması için
            // WorkerService'lerde bu ayarlar hazır bir şekilde geliyor.
            // AppSettings Json'ı yükleme işlemi
            // Eğer connection verirsek bu alanı kullanmaya gerek kalmaz.

            
            if (_connection == default(DbConnection))
            {
                DbInitializer.Build();
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseLazyLoadingProxies()
     .UseSqlServer(DbInitializer.Configuration.GetConnectionString("SqlCon"));

            }
            else
            {
                //Loglama ve Lazy Loading
                // Trace , Debug , Info , Warning , Error , Critical
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseLazyLoadingProxies()
                      .UseSqlServer(_connection);
                // No Tracking olarak belirtebiliyoruz.
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }



        }

        // SaveChange çağrılmadan önce CreatedDate otomatik olarak oluşsun.
        //public override int SaveChanges()
        //{
        //    ChangeTracker.Entries().ToList().ForEach(x =>
        //    {
        //        if (x.Entity is Product product)
        //        {
        //            if (x.State == EntityState.Added)
        //            {
        //                product.CreatedDate = DateTime.Now;
        //            }
        //        }
        //    });

        //    return base.SaveChanges();
        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 2. yol OnModelCreating üzerinden
            //modelBuilder.Entity<Product>().ToTable("ProductTBB","productsTbb");

            // Belirli bir karakter belirtmek için IsFixedLength true . Migration oluşurken OnModelCreating baz alınıyor.
            //modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(15).IsFixedLength(true);

            // Has ile başlanıp daha sonrasında With ile ilişkiyi kendimiz kurabiliriz.
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

            // OneToOne ilişki ProductFeature Id'si hem PK hem FK
            //modelBuilder.Entity<Product>().HasOne(x => x.ProductFeature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x=>x.Id);

            // Default olarak Ara tablo ekleme.
            //modelBuilder.Entity<Student>()
            //    .HasMany(x => x.Teachers)
            //    .WithMany(x => x.Students)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "StudentTeacherManyToMany",
            //    y => y.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId").HasConstraintName("FK__TeacherId"),
            //    x=>x.HasOne<Student>().WithMany().HasForeignKey("StudentId").HasConstraintName("FK__StudentId")
            //    );


            // Category silindiğinde ürünlerde silinecek.
            modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // UnitPrice ve KDV alanını çarp TotalPrice'a yaz. 
            modelBuilder.Entity<Product>().Property(x => x.TotalPrice).HasComputedColumnSql("[UnitPrice]*[Kdv]");


            // TPT davranışı için -> Her Entity' karşılık bi Tablo oluşturuyor.
            modelBuilder.Entity<BasePerson>().ToTable("Persons");
            modelBuilder.Entity<Employee>().ToTable("Employees");


            // Owned Type -> Burada Property Kolon isimlerinide düzenledik.
            modelBuilder.Entity<Manager>().ToTable("Managers").OwnsOne(x => x.OwnedType, x =>
            {
                x.Property(x => x.CreatedTime).HasColumnName("CreatedTime");
                x.Property(x => x.ModifiedTime).HasColumnName("ModifiedTime");
            });

            // Include dediğimizde Index tablosunda UnitPrice ve Stock datasını da tutacak. Sorgulama performansı arttırıyor .
            // Disk alanı maliyetini arttırır.

            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IncludeProperties(x => new { x.UnitPrice, x.Stock });

            // Kısıtlamalar -> Örnek olarak
            modelBuilder.Entity<Product>().HasCheckConstraint("DateCheck", "[CreatedDate]>[LastAccessDate]");

            // Keyless Entity

            //modelBuilder.Entity<ProductFull>().HasNoKey();

            // ToList() dediğimizde Sql bu query'yi çağıracaktır.
            modelBuilder.Entity<ProductEssential>().ToSqlQuery("Select Id,Name,UnitPrice From Products");

            // ToView ile View'ı çağırıyoruz. HasNoKey olarak işaretliyoruz.
            modelBuilder.Entity<ProductView>().HasNoKey().ToView("ProductWithFeature");

            //Default değeri false
            modelBuilder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);

            // default'u 0 dır.
            // DbContext içerisine Barcode değerini gönderebiliriz.
            if (Barcode != default(int))
            {
                modelBuilder.Entity<Product>().HasQueryFilter(p => p.Barcode == Barcode);
            }
            else
            {
                // Where koşulunda bu IsDeleted koşulu gelecek.
                modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsDeleted == false);

            }
            // SQL'den gelen datayı karşılayacak olan bi Model olacak.
            // ToFunction ile SQL de yazılan fonksiyonu burada çağrıabiliriz. ToFunction için  Table dönücek ve herhangi bir parametre almayacak.
            modelBuilder.Entity<ProductFull>().HasNoKey().ToFunction("fc_product_full");



            // ! işareti koyarsak Design time esnasında null olmadığını söylemek için . Runtime'da etkisi yok
            // HasName ile kullanıcağımız Function ismi belirtebiliriz.
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductWithFeatures), new[] { typeof(int) })!)
                .HasName("fc_product_full_withCategory");

            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetProductCountByCategoryId), new[] { typeof(int) })!)
         .HasName("fc_product_count_scalar");

            // Key değeri olmadığını belirtiyoruz.
            modelBuilder.Entity<ProductCount>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}

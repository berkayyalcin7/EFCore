// See https://aka.ms/new-console-template for more information


using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

// AppSettings json'ı okunabilecek hale getiriyor. 
DbInitializer.Build();

using (var context = new AppDbContext())
{
    #region State Tracking işlemleri
    //var products = await context.Products.ToListAsync();

    //products.ForEach(p =>
    //{
    //    // State'i bize verir . Değişiklilk yok ise Unchanged
    //    var state = context.Entry(p).State;

    //    Console.WriteLine($"{p.Name} : {p.UnitPrice} - {p.Stock} state : {state}");
    //});


    //var newProduct = new Product
    //{
    //    Name = "Iphone 11",
    //    UnitPrice = 800,
    //    Stock = 100,
    //    Barcode = 134143
    //};
    //// Memory de track etmediği data - > Detached
    //Console.WriteLine("İlk state : " + context.Entry(newProduct).State);

    ////await context.Products.AddAsync(newProduct);
    //// Ekleme işlemi ile aynı görevi görüyor.
    ////context.Entry(newProduct).State = EntityState.Added;

    //// Memory'de track edildi Added
    //Console.WriteLine("Eklendikten sonra state : " + context.Entry(newProduct).State);

    //await context.SaveChangesAsync();

    //// DB'ye yansıdıktan sonra Unchanged olarak değişti. 
    //Console.WriteLine("Son state : " + context.Entry(newProduct).State);


    // üzerinde güncelleme yapacak isek bir data Çektik .
    //var getProduct = await context.Products.FirstAsync();
    //Console.WriteLine("İlk state : " + context.Entry(getProduct).State);

    //getProduct.UnitPrice = 900;

    //Console.WriteLine("Değiştikten sonra state : " + context.Entry(getProduct).State);

    //await context.SaveChangesAsync();

    //Console.WriteLine("Güncellendikten sonra state : " + context.Entry(getProduct).State);

    //context.Remove(getProduct);

    //await context.SaveChangesAsync();

    //// Silme işleminden sonra Unchanged değil Detached olur çünkü DB'de olmayan şey izlenmiyor.
    //Console.WriteLine("Silindikten sonra state : " + context.Entry(getProduct).State);

    // Detached durumunda bir product üzerinde herhangi bir değişiklik yapamayız.
    //context.Entry(getProduct).State = EntityState.Detached;

    //getProduct.Stock = 5000;

    //await context.SaveChangesAsync();

    //Console.WriteLine("Detachten sonra değişiklik yaparsak state : " + context.Entry(getProduct).State);


    // Update Metodu Track edilmeyen datalar için var örnek
    //context.Update(new Product() { Id = 2, Name = "Iphone 13 Pro Max", Stock = 50, Barcode = 1441231, UnitPrice = 1400 });
    //await context.SaveChangesAsync();

    #endregion


    #region Change Tracker , Update 
    // Memory'de erişilen datalara değişiklik için ChangeTracker kullanırız.

    // AsNoTracking kullanarak izlemeyi engelleyebiliriz ...
    //var productsNoTrack = await context.Products.AsNoTracking().ToListAsync();

    //context.Products.Add(new Product
    //{
    //    Name = "Kalem1",
    //    UnitPrice = 100,
    //    Stock = 500,
    //    Barcode = 4551235
    //});
    // DbContext üzerinden SaveChanges metoduna override ettik .
    //context.ChangeTracker.Entries().ToList().ForEach(x =>
    //{
    //    if (x.Entity is Product product)
    //    {
    //        if (x.State==EntityState.Added)
    //        {
    //            product.CreatedDate = DateTime.Now;
    //        }
    //    }
    //});

    // Yanlış yapılan güncelleme örneği

    //var product = context.Products.First();

    //product.Barcode = 341341;

    //// Bu metodun burada kullanımı gereksizdir. çünkü atadığımız değer Tracking tarafından Modified olarak ayarlandığı için tekrardan Update etmemize gerek yoktur.
    //// Eğer herhangi bir datayı çekmeden güncellemek istersek o zaman Update metodunu kullanabiiriz.
    //context.Update(product);

    #endregion


    //var category = new Category() { Name = "Araç Gereçler" };

    //var newProduct = new Product() { Name = "Kalem 1", UnitPrice = 100, Stock = 300, Barcode = 123,
    //    Kdv = 18,
    //    Category = category,ProductFeature=new() { 
    //Height=15,Width=20,Size="S"

    //}};

    //context.Add(newProduct);
    #region One to One
    // Product - > Parent
    // ProductFeature -> Child nesne

    //var category = context.Categories.First();
    //var product = new Product { Name = "Silgi-2", UnitPrice = 200, Stock = 100, Barcode = 150541,Kdv=18,
    // Category=category,ProductFeature=new() { 
    //Height =100,Width=100,Size="M"
    //}};

    //context.Products.Add(product);
    #endregion

    #region Many To Many

    // Öğrenci üzerinden öğretmen ekleme
    //var student = new Student() { Name = "Ahmet", Age = 21 };
    //student.Teachers.Add(new() { Name = "Işıl V" });
    //student.Teachers.Add(new() { Name = "Meral A" });
    //// tam tersi de olabilir.

    //var teacher = context.Teachers.First(x => x.Name == "Işıl V");

    //teacher.Students.Add(new() { Name = "Hasan", Age = 20 });
    //teacher.Students.Add(new() { Name = "Ayla", Age = 19 });

    #endregion

    #region Delete Davranışları

    //Cascade :  ilgili datalarda ana data ile birlikte silinir . Tehlikeli durum
    // Restrict : Silmek istediğimiz category'ye bağlı product varsa child'da bağlı data varsa silmeyi engeller
    // SetNull : ForeignKey nullable ise ve SetNull belirlemişsek ilgili satır silindimi diğer bağlı datalara Null atar
    // NoAction : EFCore hiçbir şey yapmıyor VT 'de ne belirlenirse.

    //var category = new Category()
    //{
    //    Name = "Kalem",
    //    Products = new List<Product>()
    //    {
    //        new(){Name="Kalem 1",UnitPrice=100,Stock=100,Barcode=150,Kdv=18},
    //             new(){Name="Kalem 2",UnitPrice=100,Stock=100,Barcode=150,Kdv=20},
    //                  new(){Name="Kalem 3",UnitPrice=100,Stock=100,Barcode=150,Kdv=10},


    //    }
    //};
    //context.Categories.Add(category);

    //var category = context.Categories.First();
    //// Cascade olduğu için Product'da silinecek.
    //context.Categories.Remove(category);
    #endregion

    #region Eager Loading
    //var category = new Category() { Name = "Telefonlar" };
    //category.Products.Add(new Product { Name = "SAMSUNG S21 ULTRA", UnitPrice = 899, Kdv = 10, Barcode = 134123, Stock = 50 });

    //// Include ile Eager Loading yapılabiliyor.
    //var categoryWithProducts = context.Categories.Include(c => c.Products).ThenInclude(c => c.ProductFeature).First();

    //categoryWithProducts.Products.ForEach(c =>
    //{
    //    Console.WriteLine($"{categoryWithProducts.Name} {c.Name} {c.UnitPrice}");
    //});

    #endregion

    #region Explicit Loading
    //var category = context.Categories.First();
    //var product = context.Products.First();

    //if (true)
    //{
    //    // 1 e çok ilişkide Collection ile çekebiliriz.
    //    context.Entry(category).Collection(x => x.Products).Load();

    //    category.Products.ForEach(x => Console.WriteLine(x.Name));

    //    // 1 e 1 ilişkide Reference
    //    context.Entry(product).Reference(x => x.ProductFeature).Load();
    //}
    #endregion

    #region Lazy Loading -> Virtual Keywordü Property için gerekli   
    //var category = context.Categories.First();
    // Kategoriye bağlı productlar
    //var products = category.Products;
    #endregion

    #region TBH ( Table Per Hierarchy)
    //context.Persons.Add(new Manager() { FirstName = "Berkay", LastName = "Yalçın", Grade = 1 });
    //context.Persons.Add(new Employee() { FirstName = "Ahmet", LastName = "Özgür", Salary = 1500 });
    //var managers = context.Managers.ToList();
    //var employees = context.Employees.ToList();

    //// Peki Context üzerinden Person 'ı çekmeye kalkarsak -> Hem Manager hem Employee verilerini de getirecek
    //var person = context.Persons.ToList();
    #endregion

    #region TPT ( Table Per Type)
    // Ekleme işlemi aynı şekilde oluyor . ModelBuilder üzerinden ToTable() kullanarak bütün tabloları oluşturuyoruz
    // Aralarındaki ilişkiyi EFCore otomatik olarak yansıtıyor . Diagram üzerinden görülebilir.
    // Base değerleri Base Tablosuna Diğer tablolara özgün bilgileri -> o tablolara kaydediyor
    #endregion

    #region Owned Type Entities 
    //OnModel Creating ve Entityler üzerinde işlendi.
    #endregion

    #region KeylessEntity Tipleri -> 
    //Bazı Entitylerde Primary Key olmayabilir . Bunu EFCore'a bildirmek lazım .Keyless'lar CRUD edilemez çünkü DbContext tarafından Track Edilemez.
    // PK içermeyen DbDeki viewları maplemek istersek kullanabiliriz.
    // Raw SQL cümleciklerinden dönen datayı maplemek istersek kullanabiliriz.


    //    var productFulls = context.ProductFulls.FromSqlRaw(@"select p.Id 'Product_Id',c.Name 'CategoryName', p.Name 'ProductName',p.UnitPrice,pf.Height from Products p
    //join ProductFeatures pf on p.Id=pf.Id
    //join Categories c on p.CategoryId=c.Id").ToList();
    #endregion


    #region Client vs Server Evelatuion
    //context.Users.Add(new Users() { Phone = "05385282234" });
    //context.Users.Add(new Users() { Phone = "05396586136" });


    // EfCore -> Custom metodu SQL cümleciğine döndüremez önce Listeyi çekip daha sonra işlemi yapmamız lazım. Memorye önceden alacağız
    //var usersPhone = context.Users.Where(x => FormatPhone(x.Phone) == "5385282234").ToList();

    // Hatasız gelicek ama Formatsız şekilde phone basılacak
    //var usersPhone = context.Users.ToList().Where(x => FormatPhone(x.Phone) == "5385282234");

    //var usersPhone = context.Users.ToList().Select(x=> new { Phone=FormatPhone(x.Phone)}).ToList();

    //Console.WriteLine("İşlem Başarılı");
    #endregion




    context.SaveChanges();




}

string FormatPhone(string phone)
{
    return phone.Substring(1, phone.Length - 1);
}
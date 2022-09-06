// See https://aka.ms/new-console-template for more information


using AutoMapper.QueryableExtensions;
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using EFCore.CodeFirst.DTOs;
using EFCore.CodeFirst.Mapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

// AppSettings json'ı okunabilecek hale getiriyor. 
DbInitializer.Build();

// SqlConnection -> DbConnection'dan miras aldığı için tanımlayıp Context'in içerisinde geçebiliyoruz.
var connection = new SqlConnection(DbInitializer.Configuration.GetConnectionString("SqlCon"));

//GetProducts(1, 1).ForEach(x =>
//{
//    Console.WriteLine($"{x.Id} - {x.Name}");
//});

//List<Product> GetProducts (int page,int pageSize)
//{


// Barcode değeri 12345 olan kayıtlar gelicek.
// Ctor tekrar güncellendi üstteki satır silindi

using (var context = new AppDbContext(connection))
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

    #region Insert 
    //var category = new Category() { Name = "Araç Gereçler" };

    //var newProduct = new Product() { Name = "Kalem 1", UnitPrice = 100, Stock = 300, Barcode = 123,
    //    Kdv = 18,
    //    Category = category,ProductFeature=new() { 
    //Height=15,Width=20,Size="S"

    //}};

    //context.Add(newProduct);
    #endregion

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

    #region Join Yapıları

    #region İkili Join
    //  Products ile Join işlemi
    //var result = context.Categories.Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => new
    //{
    //    CategoryName=c.Name,
    //    ProductName=p.Name,
    //    ProductPrice=p.UnitPrice
    //}).ToList();

    //var result2 = (from c in context.Categories join p in context.Products on c.Id equals p.CategoryId select p).ToList();

    #endregion

    #region 3 Tablolu Join
    // 3 tablolu join
    //var result = context.Categories
    //    .Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => new {c,p})
    //    .Join(context.ProductFeatures, x => x.p.Id, y => y.Id, (c, pf)=>new
    //    {
    //        CategoryName=c.c.Name,
    //        ProductName=c.p.Name,
    //        ProductFeature=pf.Size
    //    }).ToList();

    // Daha çok tercih edilen yöntem Okunabilirliği daha fazla
    //var result2 = (from c in context.Categories
    //               join p in context.Products on c.Id equals p.CategoryId
    //               join pf in context.ProductFeatures on p.Id equals pf.Id
    //               select new
    //               {
    //                   CategoryName = c.Name,
    //                   ProductName = p.Name,
    //                   ProductFeature = pf.Size
    //               }).ToList();

    #endregion

    #region Left-Right - Full Outer  Join Query Syntax ile
    // Left join ile yer değiştirerek RightJoin alabiliriz.
    //var leftJoinResult = await (from p in context.Products
    //                       join pf in context.ProductFeatures on p.Id equals pf.Id into pfList
    //                       // Boş olduğunda Default'ları al yani Tüm productlar gelicek.
    //                       from pf in pfList.DefaultIfEmpty()
    //                       select new { Id=p.Id,Name=p.Name,Size=pf.Size }).ToListAsync();

    //var rightJoinResult = await (from pf in context.ProductFeatures
    //                       join p in context.Products on pf.Id equals p.Id into pList
    //                       // Boş olduğunda Default'ları al yani Tüm productlar gelicek.
    //                       from p in pList.DefaultIfEmpty()
    //                       select new { Id=p.Id,Name=p.Name,Size=pf.Size }).ToListAsync();


    //// Sol ve Sağ listeyi birleştirirsek "Union" ile 
    //var outerJoin = leftJoinResult.Union(rightJoinResult);

    #endregion
    #endregion

    #region Sql Raw işlemleri 

    //var productFeatures = await context.ProductFeatures.FromSqlRaw("select * from ProductFeatures").ToListAsync();
    //var unitPrice = 500;
    ////var products = await context.Products.FromSqlInterpolated($"select * from products where UnitPrice >{unitPrice}").ToListAsync();

    //// Custome Query


    //// Kendi oluşturduğumuz ProductEssential üzerinden Product tablosundaki belirli alanları mapleyip çekiyoruz .
    //// Eğer Product üzerinden bu alanları çekmeye kalksaydık , Barcode alanı null gelemeyeceğinden Context hata fırlatacaktı.
    //var product = context.ProductEssentials.FromSqlRaw("Select Id,Name,UnitPrice from Products").ToListAsync();



    #endregion

    #region ToSqlQuery

    // ToSqlQuery ModelBuilder üzerinden belirttik
    // Where koşulunu oluşturduğumuz ToSqlQuery içindeki sorguya dahil eder
    var products = context.ProductEssentials.Where(x => x.UnitPrice > 150).ToList();

    #endregion

    #region ToView çağırma
    var productView = context.ProductView.Where(x => x.Width > 50).ToList();

    // HasNoKey koyulan tüm entityler Track Edilmez -> Bu  datalar Maplenmediği için böyle bir kayıt EF Core tarafında hata vericek. View' a EF Core tarafında verieklemek sağlıklı değil
    //context.ProductView.Add(new ProductView() { ProductName = "XTF 500", CategoryName = "Hatalı Kayıt", Height = 50, Width = 100 });

    #endregion

    #region Sayfalama Metotları 

    // Take Metodu
    // Skip Metodu

    // ilk 3 datayı vericek. skip: 0 atla take olarak 3 ver

    // page 1 , pageSize 3  -> (page-1)*pageSize)
    // page 2 gönderirsek ilk 3 ünü atla sonraki 3 ü ver

    //return context.Products.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();


    #endregion

    #region Soft Delete IsDeleted
    // OnConfiguring kısmında ayarlamalar yapıldı
    // IgnoreQueryFilters() ile tanımlamayı iptal edebiliriz.
    //var productsWithIsDeletedFalse = context.Products.ToList();




    #endregion

    #region Multi Tenancy

    // AppDBContext içerisinde anlatıldı.

    #endregion

    #region Query Tags Logging

    //var productsWithFeat = context.Products.TagWith("Bu Query Ürünler ve Ürünlere Bağlı Özellikleri Getirir").Include(x => x.ProductFeature).Where(x => x.UnitPrice > 50).ToList();

    #endregion

    #region Tracking NonTracking

    //var product = context.Products.AsNoTracking().FirstOrDefault();
    //// AsNoTracking kullandığımız için ek olarak Update metodunu kullanmamız gerekiyor.
    //product.UnitPrice = 150;
    //context.Update(product);




    #endregion

    #region Stored Procedure Giriş

    // SP'nin Modele Map'li olması gerekiyor
    //var spProducts = await context.Products.FromSqlRaw("exec sp_get_products").ToListAsync();

    // Where Koşulu burada kullanamıyoruz. Datayı çektikten sonra Koşulları kullanabiliriz.
    //var spFull = await context.ProductFulls.FromSqlRaw("exec sp_get_product_full").ToListAsync();

    // Parametre ile SP Çağırma FromSqlInterpolated tercih edilir.
    //var productsWithParameter = await context.ProductFulls.FromSqlInterpolated($"exec sp_get_product_full_parameters {8},{150}").ToListAsync();
    #endregion

    #region CRUD Stored Procedure İşlemleri
    //var product = new Product()
    //{
    //    Name = "Intel i5 11th 11600",
    //    UnitPrice = 3150,
    //    Stock = 100,
    //    Barcode = 13211,
    //    Kdv = 1,
    //    CategoryId = 9
    //};

    // Geriye ProductId döneceğiz. bu yüzden Direction olarak Output olarak bize dönücek.
    //var newProductIdSql = new SqlParameter("@newId", System.Data.SqlDbType.Int);
    //newProductIdSql.Direction = System.Data.ParameterDirection.Output;
    //context.Database.ExecuteSqlInterpolated($"exec sp_insert_products_returnProductId {product.Name},{product.UnitPrice},{product.Stock},{product.Barcode},{product.Kdv},{product.CategoryId},{newProductIdSql} out");
    // Değeri atadık.
    //var newProductId = newProductIdSql.Value;

    #endregion

    #region Tablo dönen Function 

    // Fonksiyon  ToFunction() metodu içinde yer alan fonksiyonu çağrıcaktır.
    //var result = await context.ProductFulls.ToListAsync();

    //int categoryId = 9;
    //var productswithFeature = context.ProductWithFeatures.FromSqlInterpolated($"select * from fc_product_full_withCategory({categoryId})").ToList();

    #endregion

    #region Scalar Valued Functionlar
    // Metodu çağırıyoruz.
    // Functionlar'da Ef core üzerinde Where ifadeleri ile çalışıyor lakin Stored Procedurlerde bu çalışmaz.
    var productFunc = context.GetProductWithFeatures(1).Where(x => x.Width > 100).ToList();

    // Scalar Valued Function -
    // context.GetProductCountByCategoryId bağımsız şekilde kullanamıyoruz. Bağımsız olarak kullanmak ister isek bir Modelle MAP işlemi yapmak gerekiyor.
    var categories = context.Categories.Select(x => new
    {
        CategoryName = x.Name,
        ProductCount = context.GetProductCountByCategoryId(x.Id)
    });

    // Map ile kullanma
    var categoryId = 9;
    // .Count olan değer count property'si , metodu değil !!!
    // as Count property ismi ile aynı olmak zorunda.
    var productCount = context.ProductCount.FromSqlInterpolated($"SELECT DBO.fc_product_count_scalar({categoryId}) as Count").First().Count;

    #endregion

    #region Anonymous Type'lar


    // Eğer Select ifadesini kullanıyorsak Include ifadelerini yazmamıza gerek yok . Ama Navigation Propertyler olmak zorunda.
    //var productsAnonymous = context.Products.Select(x => new
    //{
    //    CategoryName=x.Category.Name,
    //    ProductName=x.Name,
    //    ProductPrice=x.UnitPrice,
    //    width=(int?)x.ProductFeature.Width
    //}).Where(x=>x.width > 50).ToList();




    #endregion

    #region DTO (Console) / ViewModel (ASP tarafı)

    // DTO ile tanımlanması burada AutoMapper kullanmadık.
    //var productDTO = context.Products.Select(x => new ProductDto
    //{
    //    CategoryName = x.Category.Name,
    //    ProductName = x.Name,
    //    ProductPrice = x.UnitPrice,
    //    Width = (int?)x.ProductFeature.Width
    //}).Where(x => x.Width > 50).ToList();



    //var productList = context.Products.ToList();

    //  ProductMapper DTO'ya mapledik.
    //  Sadece MapperDTO içinde olan propertyleri alır. Ama Tüm datayı çektikten sonra DTO'ya çevirdik.
    //var productMap= ObjectMapper.Mapper.Map<List<ProductMapperDto>>(productList);


    // !!! Bunu kullanmak daha mantıklı
    // ProjectTo'ya direkt bir DTO vereceğiz otomatik olarak , hangi propertyler varsa onları vericek.
    // Tüm data yerine sadece olan dataları getirir. Ek olarak Where koşulunu kullanabiliriz.
    var projectDto = context.Products.ProjectTo<ProductMapperDto>(ObjectMapper.Mapper.ConfigurationProvider).Where(x => x.UnitPrice > 50).ToList();






    #endregion

    #region Transaction Mantığı

    // SaveChanges transaction yapısı -> 1 ' den fazla SaveChanges'i aynı yerde kullanıyorsak Transaction yapısını elle yazmamız gerekiyor
    // Burada 1 tanesi yansımasa dahi EF Core tarafında tüm işlem geri alınıyor.
    // Loglama veya özel bir işlem olmayacaksa Try catch içerisine alınmasına gerek yok.
    using (var transaction = context.Database.BeginTransaction())
    {
        var categoryTransact = new Category { Name = "Telefonlar" };

        context.Categories.Add(categoryTransact);

        context.SaveChanges();

        var product = context.Products.First();

        var newProduct = new Product
        {
            Name = "Iphone 11",
            // Yukarıda tanımlı kategorinin Id'sini kullanıyoruz. Lakin Yukarıdaki Kategori SaveChangeso olmadan Id Değeri gelmez
            // Bunun yerine Navigation Property'si ile yukarıdaki categoryTransact'i verebiliriz.
            //CategoryId = categoryTransact.Id,
            CategoryId = 999,
            UnitPrice = 990,
            Kdv = 1,
            Barcode = 1234,
            Stock = 200

        };

        product.Name = "Updated Data 123";
        // DB'de olmayan bir Id veriyoruz.
        product.CategoryId = 20;
        // EF Core 2 değişikliği tek bir çağrı ile gönderiyor ,
        // 2 işlemden birinde hata meydana geldiyse EF Core Rollback işlemine dönüyor
        // 1 metodun içinde birden fazla SaveChanges kullanılırsa Transaction'u açık açık belirtmek gerekiyor.
        context.SaveChanges();

        // 2 ayrı context'te Transaction kullanma
        // Bu farklı bir DB'de olabilir.
        using (var dbContext2 = new AppDbContext(connection))
        {
            // üstteki Transaction'ı kullanıyoruz . 
            // Transaction using blokları içindeyse using bloğu bittiğinde Transaction Dispose olur . yani farklı Contextleri Using Transaction blokları içerisinde kullanmak daha mantıklı. 
            dbContext2.Database.UseTransaction(transaction.GetDbTransaction());
        }



        transaction.Commit();
    }





    #endregion









}

/*}*/



string FormatPhone(string phone)
{
    return phone.Substring(1, phone.Length - 1);
}
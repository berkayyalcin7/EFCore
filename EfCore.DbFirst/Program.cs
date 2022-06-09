
using EfCore.DbFirst.DAL;
using Microsoft.EntityFrameworkCore;


// 1 kere initialize edilsin.
DbContextInitializer.Build();

using (var context = new AppDbContext())
{
    var products = await context.Products.ToListAsync();

    products.ForEach(p =>
    {
        Console.WriteLine($"{p.Name} : {p.Price}");
    });

}
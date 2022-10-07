using EFCoreConcurrency.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreConcurrency.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> List()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);


            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                // Thread bloklanmaz . Aktif şekilde kullanılır.
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                // Hatalı olan ilk satırı aldık
                var exceptionEntry = exception.Entries.First();
                // hata esnasında var olan değerleri aldık
                var clientValues = exceptionEntry.CurrentValues;
                // db'deki değerleri alır.
                var dbValues = exceptionEntry.GetDatabaseValues();
        



                if (dbValues ==null)
                {
                    ModelState.AddModelError(string.Empty, "Bu ürün başka bir kullanıcı tarafından silindi ...");
                }
                else
                {
                    // Product'a çevirdik
                    var databaseProducts = dbValues.ToObject() as Product;
                    // Güncellenen Değerleri gösterebiliriz
                    ModelState.AddModelError(string.Empty, "Bu değerler başka bir kullanıcı tarafından güncellenmiş.");
                    ModelState.AddModelError(string.Empty, $"Güncellenen Değer : Name : {databaseProducts.Name}, Price : {databaseProducts.Price} , Stock : {databaseProducts.Stock}");
                }

                // Aynı modeli geri dön
                return View(product);
            }
            
        }

    }
}

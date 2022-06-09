using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DbFirst.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext()
        {

        }

        // Parametre olarak bi ctor tanımlar isek default olarak ta parametresiz ctor tanımlamak gerekir. (Kullanım açısından)
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        
        // appsettingsJson' oku
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbContextInitializer.Configuration.GetConnectionString("SqlCon"));
        }


    }
}

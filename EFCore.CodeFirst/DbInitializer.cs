using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst
{
    public class DbInitializer
    {
        public static IConfigurationRoot Configuration;
        // veritabanı option belirteceğimiz yer
      
        public static void Build()
        {
            // uygulamamnın çalışmış olduğu klasörü al
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // okuyabileceğimiz dosyayı hazır hale getiriyoruz.
            Configuration = builder.Build();


            //OptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //// SqlConnection oku.
            //OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlCon"));

        }
    }
}

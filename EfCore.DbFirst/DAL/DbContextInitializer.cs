using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DbFirst.DAL
{
    public class DbContextInitializer
    {
        public static IConfigurationRoot Configuration;
        // veritabanı option belirteceğimiz yer
        public static DbContextOptionsBuilder<AppDbContext> OptionsBuilder;

        public static void Build()
        {
            // uygulamamnın çalışmış olduğu klasörü al
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);

            // okuyabileceğimiz dosyayı hazır hale getiriyoruz.
            Configuration = builder.Build();


            //OptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //// SqlConnection oku.
            //OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlCon"));

        }


    }
}

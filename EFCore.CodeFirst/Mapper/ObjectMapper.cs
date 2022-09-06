using AutoMapper;
using EFCore.CodeFirst.DAL;
using EFCore.CodeFirst.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.Mapper
{
    public class ObjectMapper
    {

        // configurasyonu yapılmış mapper 'ı dönüyor
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>( () =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();

        });

        // IMapper'a erişmesi için ... 
        // Ne zaman ObjectMapper üzerinden Mapper'a erişirsek çalışacak . 
        public static IMapper Mapper => lazy.Value;
    }

    // Api ve MVC uygulamalarında aynı
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<ProductMapperDto, Product>().ReverseMap();
        }

    }
}

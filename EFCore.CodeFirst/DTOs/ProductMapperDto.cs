using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DTOs
{
    public class ProductMapperDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string ProductUrl { get; set; }
        public decimal UnitPrice { get; set; }
        // Sql tarafında Kdv ve Price çarpımı sonucu vericek . EFCore bu alanı göndermesin.
        public decimal TotalPrice { get; set; }
    }
}

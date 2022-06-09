using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // virtual - Lazy loading kısımlarında önemli   null ex hatası almamak için nesne örneği
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

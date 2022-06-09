using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    // One-to-one Product ile
    public class ProductFeature
    {
        //Hem PK, Hem FK
        [Key,ForeignKey("Product")]
        public int Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Size { get; set; }

        // ProductFeature üzerinden Product eklenmemesini istiyorsak NavigationProp ları kapayabiliriz. 

        //public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

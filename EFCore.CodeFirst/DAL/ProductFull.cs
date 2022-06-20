using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{

    [Keyless]
    public class ProductFull
    {
        public int Product_Id { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Height { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    [Owned] // Veya FluentApi ile tanımlanambilir OnModelCreating de
    public class OwnedType
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}

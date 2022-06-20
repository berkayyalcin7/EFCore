using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Manager:BasePerson
    {
        public int Grade { get; set; }

        public OwnedType OwnedType { get; set; }    
    }
}

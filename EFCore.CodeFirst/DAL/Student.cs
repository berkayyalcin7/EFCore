using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
        // EFcore 5.0 üzerinde ManyToMany ilişkide Ara tabloyu kendi oluşturuyor.
        public virtual List<Teacher> Teachers { get; set; } = new();
        
    }
}

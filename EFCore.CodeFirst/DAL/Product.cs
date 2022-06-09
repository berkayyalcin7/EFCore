using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    // Validation kullanıcaksak Data Annotation kullanmak mantıklı  -> 3rd Party Fluent Validation
    //[Table("ProductTbl",Schema ="Products")]

    // Virtual SQL serverdan aldığı dataları basıyor .. LazyLoading de Virtual keywordlerine ihtiyacımız var.
    public class Product
    {
        public int Id { get; set; }

        // Tipi ve Kolon adını Annotations üzerinden de belirtebiliriz. ! Order çalışması için Tablonun sıfırdan oluşması gerekiyor !
        //[Column("Name2",TypeName ="nvarchar(100)",Order =1)]
        //[StringLength(100,MinimumLength =2)]
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Barcode { get; set; }

        // Virgülden sonra kaç karakter olacağını bleirtiyoruz
        [Precision(18,2)]
        public decimal UnitPrice { get; set; }


        // sadece insert işlemlerinde Vt'ye yansıt. -> Update işlemlerine dahil etmesin.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        // Eklerken güncellerken VT'de kendimiz yapıyoruz burada yapılacak işlemler vt ye yansımayacak.
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastAccessDate { get; set; }

        public int Kdv { get; set; }

        // Sql tarafında Kdv ve Price çarpımı sonucu vericek . EFCore bu alanı göndermesin.
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }

        public int CategoryId { get; set; }
        //Navigation property -> CategoryId ismi farklı bi isimde olsa idi Annotationda bunu belirtildik.
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ProductFeature ProductFeature { get; set; }


        // Veritabanına yansıtmadan önce silmek için

        // remove-migration ile silebiliriz.

        // Veritabanına yansıttıktan sonra ise

        // update-database ÖncekiMigration

        // daha sonrasında ise remove-migration diyebiliriz.

    }
}

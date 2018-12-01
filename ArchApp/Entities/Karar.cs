using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Kararlar")]
    public class Karar : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(75)]
        public string MahkemeAdi { get; set; }
        [StringLength(50)]
        public string MahkemeTuru { get; set; }
        public DateTime KararTarihi { get; set; }        
        public string EsasNo { get; set; }
        public string KararNo { get; set; }
        public string DergiAdi { get; set; }
        public int? DergiCiltNo { get; set; }
        public int? MDergiSayiNo { get; set; }
        public int? MSayfaNo { get; set; }
        public string IctihatProgramAdi { get; set; }
        public int KategoriId { get; set; }

        public virtual Kaynak Kaynak { get; set; }



    }
    
}
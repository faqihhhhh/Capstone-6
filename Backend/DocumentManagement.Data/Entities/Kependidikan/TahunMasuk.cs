using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Entities
{
    public class TahunMasuk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThnMasukId { get; set; }
        public int ThnMasuk { get; set; } // Contoh: 2020
        public int TotalRegistrasiMhs { get; set; } // Contoh: 140
        
        // Foreign Key untuk TahunAkademik
        public int ThnAkademikId { get; set; }
        public TahunAkademik TahunAkademik { get; set; }
        
        // Relasi 1-to-1 dengan InfoMahasiswa
        public InfoMahasiswa InfoMahasiswa { get; set; }

    }
}

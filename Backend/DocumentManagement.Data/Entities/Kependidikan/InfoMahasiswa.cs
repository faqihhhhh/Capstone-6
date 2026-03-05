using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Entities
{
    public class InfoMahasiswa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InfoMahasiswaId { get; set; }

        //Atribut-atribut di Kependidikan
        public int JmlhLulus { get; set; }
        public int JmlhNonAktif { get; set; }
        public int JmlhDO { get; set; }
        public int JmlhUndurDiri { get; set; }
        
        [NotMapped]
        public int JmlhAktif
        {
            get
            {
                return TahunMasuk != null ? TahunMasuk.TotalRegistrasiMhs - (JmlhLulus + JmlhNonAktif + JmlhDO + JmlhUndurDiri) : 0;
            }
        }
        
        public float RataanIPKTotal { get; set; }
        public int MasaStudiDibawah8 { get; set; }
        public int MasaStudi8Sampai10 { get; set; }
        public int MasaStudiDiatas10 { get; set; }
        public int JumlahIPKDibawah2 { get; set; }
        public int JumlhMBKM { get; set; }

        // Foreign Key untuk TahunMasuk
        public int ThnMasukId { get; set; }
        public TahunMasuk TahunMasuk { get; set; }
    }
}

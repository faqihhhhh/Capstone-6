using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Entities
{
    public enum SemesterType
    {
        Ganjil,
        Genap
    }

    public class TahunAkademik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThnAkademikId { get; set; }
        public int ThnAkademik { get; set; }
        public SemesterType Semester { get; set; }

        [NotMapped]
        public string ThnAkademikDesc
        {
            get
            {
                int tahunBerikutnya = this.ThnAkademik + 1;
                return $"{this.ThnAkademik}/{tahunBerikutnya}-{this.Semester}";
            }
        }

        // Relasi 1-to-1 untuk Data Prestasi Mahasiswa (Kemahasiswaan)
        public PrestasiMhs PrestasiMhs { get; set; }

        // Relasi 1-to-Many dengan TahunMasuk
        public ICollection<TahunMasuk> TahunMasuks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Entities
{
    public enum KategoriPublikasi
    {
        Scopus,
        InternationalNonScopus,
        Sinta,
        HKI
    }

    public enum PenerapanMasyarakat
    {
        Ya,
        Tidak
    }
    public class Publikasi
    {
        [Key]
        public Guid PublikasiId { get; set; }

        [Required]
        public int TahunPublikasi { get; set; }

        [Required]
        [MaxLength(255)]
        public string JudulPublikasi { get; set; }

        [Required]
        public KategoriPublikasi KategoriPublikasi { get; set; }

        [Required]
        public PenerapanMasyarakat PenerapanMasyarakat { get; set; }

        // Relasi Many-to-One dengan Pegawai
        [ForeignKey("PegawaiId")]
        public Guid PegawaiId { get; set; }
        public virtual Pegawai Pegawai { get; set; }
    }
}

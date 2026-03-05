using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class CreatePublikasiDTO
    {
        [Required]
        public Guid PegawaiId { get; set; }

        [Required]
        public int TahunPublikasi { get; set; }

        [Required]
        [MaxLength(255)]
        public string JudulPublikasi { get; set; }

        [Required]
        public KategoriPublikasi KategoriPublikasi { get; set; }

        [Required]
        public PenerapanMasyarakat PenerapanMasyarakat { get; set; }
    }
}

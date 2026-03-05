using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class CreateKegiatanPPMDTO
    {
       
        [Required]
        public string JudulPPM { get; set; }

        [Required]
        public int TahunMulai { get; set; }

        [Required]
        public int TahunSelesai { get; set; }

        [Required]
        public Jenis_PPM JenisPPM { get; set; } // "Penelitian" atau "Pengabdian"

        [Required]
        public Mitra_PPM MitraPPM { get; set; } // "Pemerintah", "Swasta", atau "Luar Negeri"

        public string NomorKontrak { get; set; }

        [Range(0, Double.MaxValue)]
        public decimal DanaPPM { get; set; }

    }
}
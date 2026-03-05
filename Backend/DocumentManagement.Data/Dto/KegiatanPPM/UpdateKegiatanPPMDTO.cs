using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{ 
    public class UpdateKegiatanPPMDTO
    {
    //public Guid KegiatanID { get; set; }
    public string JudulPPM { get; set; }
    public int TahunMulai { get; set; }
    public int TahunSelesai { get; set; }
    public Jenis_PPM JenisPPM { get; set; } // "Penelitian" atau "Pengabdian"
    public Mitra_PPM MitraPPM { get; set; } // "Pemerintah", "Swasta", atau "Luar Negeri"
    public string NomorKontrak { get; set; }
    public decimal DanaPPM { get; set; }
    }
}
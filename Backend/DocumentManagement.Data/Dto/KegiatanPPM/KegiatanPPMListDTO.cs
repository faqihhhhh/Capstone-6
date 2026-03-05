using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class KegiatanPPMListDTO
    {
        public Guid KegiatanID { get; set; }
        public string JudulPPM { get; set; }
        public int TahunMulai { get; set; }
        public int TahunSelesai { get; set; }
        public string JenisPPM { get; set; } // "Penelitian" atau "Pengabdian"
        public string MitraPPM { get; set; } // "Pemerintah", "Swasta", atau "Luar Negeri"
    }
}

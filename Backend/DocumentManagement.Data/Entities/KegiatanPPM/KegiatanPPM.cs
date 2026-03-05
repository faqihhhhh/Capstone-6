using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public enum Jenis_PPM {Penelitian, Pengabdian }
    public enum Mitra_PPM {NonMitra, Pemerintah, Swasta, LuarNegeri}
    public class KegiatanPPM
    {
        [Key]
        public Guid KegiatanID { get; set; } // Primary Key
        public string JudulPPM { get; set; }
        public int TahunMulai { get; set; }
        public int TahunSelesai { get; set; }
        public Jenis_PPM JenisPPM { get; set; } // "Penelitian" atau "Pengabdian"
        public Mitra_PPM MitraPPM { get; set; } // "Pemerintah", "Swasta", atau "Luar Negeri"
        public string NomorKontrak { get; set; }
        public decimal DanaPPM { get; set; }

        
        // Relasi Many-to-Many dengan Pegawai (khusus Dosen)
        public virtual ICollection<PegawaiKegiatanPPM> PegawaiKegiatanPPMs { get; set; }
    }
}
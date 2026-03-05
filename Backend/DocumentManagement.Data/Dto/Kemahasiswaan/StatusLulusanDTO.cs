using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Data.Dto
{
   public class StatusLulusanDTO
    {
        [Required]
        public int Tahun_Input { get; set; }
        
        [Required]
        public int Bekerja { get; set; }
        
        [Required]
        public int LanjutStudi { get; set; }
        
        [Required]
        public int Internship { get; set; }
        
        [Required]
        public int Berwirausaha { get; set; }
        
        [Required]
        public int BelumKerja { get; set; }

        // Relasi 1-to-1 dengan entitas lainnya
        public MasaTungguKerjaDTO MasaTungguKerja { get; set; }
        public JenisTempatKerjaDTO JenisTempatKerja { get; set; }
        public PosisiJabatanDTO PosisiJabatan { get; set; }
        public TingkatTempatKerjaDTO TingkatTempatKerja { get; set; }
    }
    public class JenisTempatKerjaDTO
    {
        public int BUMN { get; set; }
        public int Organisasi_Multilateral { get; set; }
        public int Instansi_Pemerintah { get; set; }
        public int Organisasi_NonProfit { get; set; }
        public int Wirausaha { get; set; }
        public int Lainnya { get; set; }
    }
    public class MasaTungguKerjaDTO
    {
        public int Diatas6Bulan { get; set; }
        public int Dibawah6Bulan { get; set; }
    }
    public class PosisiJabatanDTO
    {
        public int Founder { get; set; }
        public int CoFounder { get; set; }
        public int Staf { get; set; }
        public int Freelancer { get; set; }
    }
    public class TingkatTempatKerjaDTO
    {
        public int Lokal { get; set; }
        public int Nasional { get; set; }
        public int MultiNasional { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data
{
    public class JenisTempatKerja
    {
        //------------------------ Key dan Relasi -------------------------------//
        [Key, ForeignKey("StatusLulusan")]
        public int StatusId { get; set; } // Primary Key dan Foreign Key 
        public StatusLulusan StatusLulusan { get; set; } // Navigation Property
        // ---------------------------------------------------------------------//

        // ----------------------- Atribut Entitas -----------------------------//
        public int BUMN { get; set; }
        public int Organisasi_Multilateral { get; set; }
        public int Instansi_Pemerintah { get; set; }
        public int Organisasi_NonProfit { get; set; }
        public int Wirausaha { get; set; }
        public int Lainnya { get; set; }

        // ---------------------------------------------------------------------//

        // -------------------- Calculated Property ----------------------------//
        [NotMapped]
        public double Presentase_BUMN
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
        (double)BUMN / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Organisasi_Multilateral
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Organisasi_Multilateral / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Instansi_Pemerintah
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Instansi_Pemerintah / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Organisasi_NonProfit
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Organisasi_NonProfit / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Wirausaha
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Wirausaha / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Lainnya
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Lainnya / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        // ---------------------------------------------------------------------//
    }
}

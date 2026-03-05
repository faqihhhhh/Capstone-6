using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data
{
    public class TingkatTempatKerja
    {
        //------------------------ Key dan Relasi -------------------------------//
        [Key, ForeignKey("StatusLulusan")]
        public int StatusId { get; set; } // Primary Key dan Foreign Key 
        public StatusLulusan StatusLulusan { get; set; } // Navigation Property
        // ---------------------------------------------------------------------//

        // ----------------------- Atribut Entitas -----------------------------//
        public int Lokal { get; set; }
        public int Nasional { get; set; }
        public int MultiNasional { get; set; }

        // ---------------------------------------------------------------------//

        // -------------------- Calculated Property ----------------------------//
        [NotMapped]
        public double Presentase_Lokal
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
        (double)Lokal / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Nasional
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Nasional / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_MultiNasional
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)MultiNasional / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }  
        // ---------------------------------------------------------------------//
    }
}
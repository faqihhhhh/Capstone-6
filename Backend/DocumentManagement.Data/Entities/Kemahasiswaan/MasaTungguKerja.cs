using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public class MasaTungguKerja
    {
        //------------------------ Key dan Relasi -------------------------------//
        [Key, ForeignKey("StatusLulusan")]
        public int StatusId { get; set; } // Primary Key dan Foreign Key 
        public StatusLulusan StatusLulusan { get; set; } // Navigation Property
        // ---------------------------------------------------------------------//

        // ----------------------- Atribut Entitas -----------------------------//
        public int Diatas6Bulan { get; set; }
        public int Dibawah6Bulan { get; set; }

        // ---------------------------------------------------------------------//

        // -------------------- Calculated Property ----------------------------//
        [NotMapped]
        public double Presentase_diatas6Bulan
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
        (double)Diatas6Bulan / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_dibawah6Bulan
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Dibawah6Bulan / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        // ---------------------------------------------------------------------//
    }
}

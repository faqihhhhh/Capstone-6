using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data
{
    public class PosisiJabatan
    {
        //------------------------ Key dan Relasi -------------------------------//
        [Key, ForeignKey("StatusLulusan")]
        public int StatusId { get; set; } // Primary Key dan Foreign Key 
        public StatusLulusan StatusLulusan { get; set; } // Navigation Property
        // ---------------------------------------------------------------------//

        // ----------------------- Atribut Entitas -----------------------------//
        public int Founder { get; set; }
        public int CoFounder { get; set; }
        public int Staf { get; set; }
        public int Freelancer { get; set; }

        // ---------------------------------------------------------------------//

        // -------------------- Calculated Property ----------------------------//
        [NotMapped]
        public double Presentase_Founder
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
        (double)Founder / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_CoFounder
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)CoFounder / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Staf
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Staf / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Freelancer
        {
            get
            {
                return StatusLulusan.JumlahKerja > 0 ?
            (double)Freelancer / StatusLulusan.JumlahKerja * 100 : 0;
            }
        }
        // ---------------------------------------------------------------------//
    }
}

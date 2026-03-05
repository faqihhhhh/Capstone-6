using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data;

namespace DocumentManagement.Data
{ 
    public class StatusLulusan
    {
        //------------- Key dan Relasi -------------//
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        
        [ForeignKey("TahunLulus")]
        public int TahunId { get; set; }
        // Properti relasi many-to-1 dengan entitas TahunLulus
        public virtual TahunLulus? TahunLulus { get; set; }

        // ---------- Atribut Entitas -------------//
        // Tahun memasukkan data (harus lebih besar dari tahun lulus)
        public int Tahun_Input { get; set; }
        public int Bekerja { get; set; }
        public int LanjutStudi { get; set; }
        public int Internship { get; set; }
        public int Berwirausaha { get; set; }
        public int BelumKerja { get; set; }
        // ----------------------------------------//

        // Properti relasi 1-to-1 dengan entitas lainnya
        public virtual MasaTungguKerja MasaTungguKerja { get; set; }
        public virtual JenisTempatKerja JenisTempatKerja { get; set; }
        public virtual PosisiJabatan PosisiJabatan { get; set; }
        public virtual TingkatTempatKerja TingkatTempatKerja { get; set; }

        //-----------------------------------------------------------------//

        // ---------- Calculated Property ---------------------------------//
        [NotMapped]
        public int JumlahMahasiswa
        {
            get 
            { 
                return Bekerja + LanjutStudi + Internship + Berwirausaha + BelumKerja;
            }
        }
        [NotMapped]
        public int JumlahKerja
        {
            get
            {
                return Bekerja + Internship + Berwirausaha;
            }
        }
        [NotMapped]
        public double Presentase_Bekerja
        {
            get
            {
                return JumlahMahasiswa > 0 ? (double)Bekerja / JumlahMahasiswa * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_LanjutStudi
        {
            get
            {
                return JumlahMahasiswa > 0 ? (double)LanjutStudi / JumlahMahasiswa * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Internship
        {
            get
            {
                return JumlahMahasiswa > 0 ? (double)Internship / JumlahMahasiswa * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_Wirausaha
        {
            get
            {
                return JumlahMahasiswa > 0 ? (double)Berwirausaha / JumlahMahasiswa * 100 : 0;
            }
        }
        [NotMapped]
        public double Presentase_BelumKerja
        {
            get
            {
                return JumlahMahasiswa > 0 ? (double)BelumKerja / JumlahMahasiswa * 100 : 0;
            }
        }
        // ---------------------------------------------------------------------//
    }
}
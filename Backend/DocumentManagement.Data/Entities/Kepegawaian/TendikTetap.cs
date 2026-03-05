using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public class TendikTetap
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Tendik")]
        public Guid TendikId { get; set; }
        
        //Atribut Tendik Tetap (PNS)
        public string Jabatan { get; set; }
        public string Golongan { get; set; }
        public DateTime? TMT { get; set; }

        // Properti hanya-baca untuk Tanggal_Pensiun
        public DateTime? Tanggal_Pensiun
        {
            get
            {
                return Tendik.Pegawai.Tanggal_Lahir.AddYears(58);
            }
        }

        // Properti hanya-baca untuk Proyeksi
        public string Proyeksi
        {
            get
            {
                if (Tanggal_Pensiun.HasValue)
                {
                    return DateTime.Now.AddYears(5) <= Tanggal_Pensiun.Value ? "Aktif" : "Pensiun";
                }
                return null;
            }
        }
        public virtual Tendik Tendik { get; set; }
    }
}
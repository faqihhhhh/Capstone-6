using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public enum Jabatan {AsistenAhli, Lektor, LektorKepala, Profesor}

    public class DosenTetap
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Dosen")]
        public Guid DosenId { get; set; }

        public string NIDN { get; set; }
        public Jabatan Jabatan_Akademik { get; set; }
        public string Golongan { get; set; }
        public string Nomor_Serdos { get; set; }
        public DateTime TMT { get; set; }

        // Properti terhitung (calculated property)
        public DateTime? Tanggal_Pensiun
        {
            get
            {
                    int retirementAge = Jabatan_Akademik == Jabatan.Profesor ? 70 : 65;
                    return Dosen.Pegawai.Tanggal_Lahir.AddYears(retirementAge);
                
            }
        }
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
        public virtual Dosen Dosen { get; set; }
    }
}
using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class UpdateKependidikanDTO
    {
        // Input untuk identifikasi TahunAkademik dan TahunMasuk
        public int ThnAkademik { get; set; }
        public SemesterType Semester { get; set; }
        public int ThnMasuk { get; set; }

        // Input untuk memperbarui InfoMahasiswa
        public int JmlhAktif { get; set; }
        public int JmlhLulus { get; set; }
        public int JmlhNonAktif { get; set; }
        public int JmlhDO { get; set; }
        public int JmlhUndurDiri { get; set; }
        public float RataanIPKTotal { get; set; }
        public int JumlahIPKDibawah2 { get; set; }
        public int MasaStudiDibawah8 { get; set; }
        public int MasaStudi8Sampai10 { get; set; }
        public int MasaStudiDiatas10 { get; set; }
    }
}

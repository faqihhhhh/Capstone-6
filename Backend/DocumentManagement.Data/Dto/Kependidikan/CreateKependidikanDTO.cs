using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
   public class CreateKependidikanDTO
    {
        // Input untuk TahunAkademik
        public int ThnAkademik { get; set; }
        public SemesterType Semester { get; set; }

        // Input untuk TahunMasuk
        public int ThnMasuk { get; set; }
        public int TotalRegistrasiMhs { get; set; }

        // Input untuk InfoMahasiswa
        public int JmlhLulus { get; set; }
        public int JmlhNonAktif { get; set; }
        public int JmlhDO { get; set; }
        public int JmlhUndurDiri { get; set; }
        public float RataanIPKTotal { get; set; }
        public int MasaStudiDibawah8 { get; set; }
        public int MasaStudi8Sampai10 { get; set; }
        public int MasaStudiDiatas10 { get; set; }
        public int JumlahIPKDibawah2 { get; set; }
        public int JumlhMBKM { get; set; }
    }
}

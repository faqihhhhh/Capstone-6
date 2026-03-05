using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class GetKependidikanDTO
    {
        // 01 Output untuk TahunAkademik
        
        public int ThnAkademik { get; set; }
        
        //public SemesterType Semester { get; set; }
        public string Semester { get; set; }
        public string Deskripsi { get; set; }

        // 02 Output untuk TahunMasuk
        public int ThnMasuk { get; set; }
        public int TotalRegistrasiMhs { get; set; }

        public InfoMahasiswaDTO InfoMahsiswa { get; set; }
    }
    public class InfoMahasiswaDTO
    {
        // 03 Output untuk InfoMahasiswa
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
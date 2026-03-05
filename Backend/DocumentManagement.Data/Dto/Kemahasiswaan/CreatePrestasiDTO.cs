using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class CreatePrestasiDTO 
    {
        public int ThnAkademik { get; set; } // Tahun Akademik untuk memasukkan data prestasi mahasiswa
        public SemesterType Semester {  get; set; }
        public PrestasiMhsDTO PrestasiMhs { get; set; } // Relasi 1-to-1

    }

    public class PrestasiMhsDTO 
    {
        public int JmlhPKM { get; set; }
        public int JumlhMapres { get; set; }
        public int JumlhLombaNasional { get; set; }
        public int JumlhLombaInter { get; set; }
        public int JumlhInbound { get; set; }
        public int JumlhOutbound { get; set; }
    }
}
using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class DeleteKependidikanDTO
    {
        public int ThnAkademik { get; set; }      // Tahun akademik, misalnya: 2024
        public SemesterType Semester { get; set; } // Semester, misalnya: Ganjil atau Genap
        public int ThnMasuk { get; set; }          // Tahun masuk mahasiswa, misalnya: 2017
    }
}

using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{ 
    public class GetTahunAkademikDTO
    {
        public int ThnAkademikId { get; set; }
        public int ThnAkademik { get; set; }
        public string Semester { get; set; }
        public string Deskripsi { get; set; } // Contoh: "2020/2021-Ganjil"
    }
}
using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class RingkasanPublikasiDTO
    {
        public string NIP { get; set; }
        public string Nama { get; set; }
        public int TotalScopus { get; set; }
        public int TotalNonScopus { get; set; }
        public int TotalSinta { get; set; }
        public int TotalHKI { get; set; }
        public int TotalPenerapanMasyarakat { get; set; }
    }
}

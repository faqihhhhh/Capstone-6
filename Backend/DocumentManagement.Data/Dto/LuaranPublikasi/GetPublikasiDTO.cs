using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class GetPublikasiDTO
    {
        public Guid PublikasiId { get; set; }
        public Guid PegawaiId { get; set; }
        public string NIP { get; set; }
        public string Nama { get; set; }
        public int TahunPublikasi { get; set; }
        public string JudulPublikasi { get; set; }
        public string KategoriPublikasi { get; set; }
        public string PenerapanMasyarakat { get; set; }
    }
}

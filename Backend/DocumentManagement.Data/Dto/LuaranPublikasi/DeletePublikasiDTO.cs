using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class DeletePublikasiDTO
    {
        public bool IsDeleted { get; set; } // Menyatakan apakah penghapusan berhasil
        public string Message { get; set; } // Pesan terkait penghapusan
    }
}

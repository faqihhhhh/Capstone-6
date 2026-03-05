using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class DeleteKegiatanPPMResponseDTO
    {
        public bool IsDeleted { get; set; }
        public string Message { get; set; }
    }
}

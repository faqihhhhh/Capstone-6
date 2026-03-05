using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class CreatePegawaiKegiatanPPMDTO
    {
        [Required]
        public Guid PegawaiID { get; set; } // ID dari Dosen yang akan dihubungkan dengan Kegiatan

        [Required]
        public Guid KegiatanID { get; set; } // ID dari KegiatanPPM yang akan dihubungkan dengan Dosen

    }
}

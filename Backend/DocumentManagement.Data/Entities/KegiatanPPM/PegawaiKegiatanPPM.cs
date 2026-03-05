using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public class PegawaiKegiatanPPM
    {
        [Key]
        public Guid Id { get; set; } // Primary Key

        public Guid PegawaiId { get; set; } // Foreign Key ke Pegawai
        public Guid KegiatanID { get; set; } // Foreign Key ke KegiatanPPM

        // Navigation properties
        public virtual Pegawai Pegawai { get; set; }
        public virtual KegiatanPPM KegiatanPPM { get; set; }
    }
}

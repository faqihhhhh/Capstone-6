using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public enum Status_Tendik { PNS, Kontrak }
    public class Tendik
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Pegawai")]
        public Guid PegawaiId { get; set; }
        
        // Atribut di Entitas Dosen
        public Status_Tendik Jenis_Tendik { get; set; }

        // Relasi opsional ke TendikTetap (null jika Jenis_Tendik bukan PNS)
        public virtual TendikTetap? TendikTetap { get; set; }
        public virtual Pegawai Pegawai { get; set; }
    }
}
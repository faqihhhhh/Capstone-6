using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public enum Status_Dosen {PNS, TetapNonPNS, Kontrak}
    public class Dosen
    {
        [Key]
        public Guid Id {  get; set; }

        [ForeignKey("Pegawai")]
        public Guid PegawaiId { get; set; }
        
        
        // Atribut dari Dosen
        public Status_Dosen Jenis_Dosen { get; set; }

        // Relasi opsional ke DosenTetap (null jika Jenis_Dosen bukan PNS)
        public virtual DosenTetap? DosenTetap { get; set; }
        public virtual Pegawai Pegawai { get; set; }
    }
}
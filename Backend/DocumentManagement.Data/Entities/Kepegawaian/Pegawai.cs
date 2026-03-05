using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data
{
    public enum Gender { Pria, Wanita }
    public enum Studi { SMA, D3, D4, S1, S2, S3 }
    public enum Nikah { Menikah, Lajang, Duda, Janda }
    public enum JenisP { Dosen, Tendik }

    public class Pegawai
    {
        public Guid Id { get; set; }    
        public string NIP { get; set; }
        public string Nama { get; set; }
        public DateTime Tanggal_Lahir { get; set; }
        public string Tempat_Lahir { get; set; }
        public Gender Jenis_Kelamin { get; set; }
        public string Alamat { get; set; }
        public string Telepon_Darurat { get; set; }
        public Studi Pendidikan_Terakhir { get; set; }
        public string Gelar { get; set; }
        public Nikah Status_Pernikahan { get; set; }
        public string Nomor_BPJS { get; set; }
        public string Nomor_NPWP { get; set; }
        public string Nomor_Paspor { get; set; }
        [Required]
        public JenisP Jenis_Pegawai { get; set; } // "Dosen" atau "Tendik"

        // Relasi dengan Dosen atau Tendik, satu harus null
        public virtual Dosen? Dosen { get; set; } // Bisa null jika Jenis_Pegawai adalah Tendik
        public virtual Tendik? Tendik { get; set; } // Bisa null jika Jenis_Pegawai adalah Dosen

        // Relasi Many-to-Many dengan entitas KegiatanPPM melalui tabel/entitas penghubung "PegawaiKegiatanPPM"
        public virtual ICollection<PegawaiKegiatanPPM> PegawaiKegiatanPPMs { get; set; }

        // Relasi One-to-Many dengan Publikasi
        public virtual ICollection<Publikasi> Publikasis { get; set; } = new List<Publikasi>();
    }
}
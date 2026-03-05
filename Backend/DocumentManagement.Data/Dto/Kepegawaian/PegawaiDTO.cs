using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Data.Dto 
{
    /// <summary>
    /// DTO untuk menerima data pegawai dari klien (Input). 
    /// Ini digunakan untuk operasi CREATE dan UPDATE pegawai beserta Dosen dan Tendik.
    /// </summary>
    public class PegawaiDTO : IValidatableObject
    {
        public Guid? Id { get; set; }  // Nullable ID, digunakan saat update, diabaikan untuk create
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
        [Required(ErrorMessage = "Jenis Pegawai is required")]
        public JenisP Jenis_Pegawai { get; set; }

        // Jika Dosen
        public Status_Dosen? Jenis_Dosen { get; set; }  // Status Dosen PNS atau non-PNS
        public DosenTetapDTO? DosenTetap { get; set; }  // Jika Dosen PNS, masukkan DosenTetap

        // Jika Tendik
        public Status_Tendik? Jenis_Tendik { get; set; }  // Status Tendik
        public TendikTetapDTO? TendikTetap { get; set; }  // Jika Tendik PNS, masukkan TendikTetap

        /// <summary>
        /// Validasi khusus untuk menentukan apakah properti yang sesuai diisi berdasarkan 
        /// jenis pegawai (Dosen atau Tendik). Ini adalah validasi kondisional.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validasi jika pegawai adalah Dosen
            if (Jenis_Pegawai == JenisP.Dosen)
            {
                if (Jenis_Dosen == Status_Dosen.PNS || Jenis_Dosen == Status_Dosen.TetapNonPNS)
                {
                    // Jika Jenis Dosen adalah PNS atau TetapNonPNS, maka DosenTetap harus diisi
                    if (DosenTetap == null)
                    {
                        yield return new ValidationResult("Data Dosen Tetap (PNS atau tetap non-pns) harus ada!.", new[] { nameof(DosenTetap) });
                    }
                }
               
            }
            // Validasi jika pegawai adalah Tendik
            if (Jenis_Pegawai == JenisP.Tendik)
            {
                if (Jenis_Tendik == Status_Tendik.PNS)
                {
                    // Jika Jenis Dosen adalah PNS atau TetapNonPNS, maka DosenTetap harus diisi
                    if (TendikTetap == null)
                    {
                        yield return new ValidationResult("Data Tendik Tetap (PNS) Harus ada!.", new[] { nameof(TendikTetap) });
                    }
                }
            }
        }
    }

    //Data Trsanfer Object untuk Entitas DosenTetap
    public class DosenTetapDTO
    {
        public Guid? DosenId { get; set; }  // FK ke Dosen, digunakan sebagai PK
        public string NIDN { get; set; }
        [Required(ErrorMessage = "Jabatan Akademik is required")]
        public Jabatan Jabatan_Akademik { get; set; }
        public string Golongan { get; set; }
        public DateTime TMT { get; set; }
        public string Nomor_Serdos { get; set; }
    }
    // DTO untuk TendikTetap, digunakan jika Tendik adalah PNS
    public class TendikTetapDTO
    {
        public Guid? TendikId { get; set; }  // FK ke Tendik, digunakan sebagai PK
        public string Jabatan { get; set; }
        public string Golongan { get; set; }
        public DateTime TMT { get; set; }
    }
}
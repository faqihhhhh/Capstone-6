using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class TendikDetailDTO
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
        
        // Atribut khusus dari Tendik
        public Status_Tendik Jenis_Tendik { get; set; }

        // TendikTetap hanya jika Tendik PNS
        public TendikTetapDetailDTO? TendikTetap { get; set; }
    }
    public class TendikTetapDetailDTO
    {
        public string Jabatan { get; set; }
        public string Golongan { get; set; }
        public DateTime? Tanggal_Pensiun { get; set; }
        public string Proyeksi { get; set; }
        public DateTime? TMT { get; set; }
    }
}
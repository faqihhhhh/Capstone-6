using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{ 
    public class RingkasanPengabdianPenelitianDTO
    {
    //public Guid PegawaiID { get; set; }
    public string NamaPegawai { get; set; }
    public string NIP { get; set; }
    public string JenisDosen {  get; set; }
    public int JumlahPenelitian { get; set; }
    public int JumlahPengabdian { get; set; }
    }
}

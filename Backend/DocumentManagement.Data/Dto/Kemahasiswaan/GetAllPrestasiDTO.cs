using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class GetAllPrestasiDTO
    {
        public int ThnAkademikId { get; set; }
        public int ThnAkademik { get; set; }
        public String Semester {  get; set; }
        public GetPrestasiMhsDTO GetPrestasiMhs { get; set; }
    }

    public class GetPrestasiMhsDTO
    {
        public int ThnAkademikId { get; set; }
        public int JmlhPKM { get; set; }
        public int JumlhMapres { get; set; }
        public int JumlhLombaNasional { get; set; }
        public int JumlhLombaInter { get; set; }
        public int JumlhInbound { get; set; }
        public int JumlhOutbound { get; set; }
    }
}

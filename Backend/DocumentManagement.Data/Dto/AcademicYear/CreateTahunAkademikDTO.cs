using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class CreateTahunAkademikDTO
    {
        public int ThnAkademik { get; set; }
        public SemesterType Semester { get; set; }
    }
}

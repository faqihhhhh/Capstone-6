using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Commands
{
    public class CreateAkademikTahunCommand : IRequest<ServiceResponse<CreateTahunAkademikDTO>>
    {
        public CreateTahunAkademikDTO TahunAkademikDTO { get; set; }

        public CreateAkademikTahunCommand(CreateTahunAkademikDTO tahunAkademikDTO)
        {
            TahunAkademikDTO = tahunAkademikDTO;
        }
    }
}
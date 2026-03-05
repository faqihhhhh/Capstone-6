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
    public class CreatePublikasiCommand : IRequest<ServiceResponse<CreatePublikasiDTO>>
    {
        public CreatePublikasiDTO PublikasiDTO { get; }

        public CreatePublikasiCommand(CreatePublikasiDTO publikasiDTO)
        {
            PublikasiDTO = publikasiDTO;
        }
    }
}

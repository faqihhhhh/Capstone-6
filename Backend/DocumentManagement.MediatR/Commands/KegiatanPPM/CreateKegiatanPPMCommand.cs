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
    public class CreateKegiatanPPMCommand : IRequest<ServiceResponse<CreateKegiatanPPMDTO>>
    {
        public CreateKegiatanPPMDTO KegiatanPPMDTO { get; set; }
        public CreateKegiatanPPMCommand(CreateKegiatanPPMDTO kegiatanPPMDTO)
        {
            KegiatanPPMDTO = kegiatanPPMDTO;
        }
    }
}
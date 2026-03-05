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
    public class UpdateKegiatanPPMCommand : IRequest<bool>
    {
        public Guid KegiatanId { get; }
        public UpdateKegiatanPPMDTO UpdateDto { get; }
        public UpdateKegiatanPPMCommand(Guid kegiatanId, UpdateKegiatanPPMDTO updateDto)
        {
            KegiatanId = kegiatanId;
            UpdateDto = updateDto;
        }
    }
}

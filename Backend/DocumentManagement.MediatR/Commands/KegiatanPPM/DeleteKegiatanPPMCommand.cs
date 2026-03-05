using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;

namespace DocumentManagement.MediatR.Commands
{
    public class DeleteKegiatanPPMCommand : IRequest<bool>
    {
        public Guid KegiatanId { get; set; }

        public DeleteKegiatanPPMCommand(Guid kegiatanId)
        {
            KegiatanId = kegiatanId;
        }
    }
}

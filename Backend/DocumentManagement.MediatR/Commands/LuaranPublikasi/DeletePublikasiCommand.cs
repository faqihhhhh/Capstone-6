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
    public class DeletePublikasiCommand : IRequest<DeletePublikasiDTO>
    {
        public Guid PublikasiId { get; }

        public DeletePublikasiCommand(Guid publikasiId)
        {
            PublikasiId = publikasiId;
        }
    }
}

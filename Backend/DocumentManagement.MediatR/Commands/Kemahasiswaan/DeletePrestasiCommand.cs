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
    public class DeletePrestasiCommand : IRequest<ServiceResponse<string>>
    {
        public int ThnAkademikId { get; set; }
        public DeletePrestasiCommand(int thnAkademikId)
        {
            ThnAkademikId = thnAkademikId;
        }

    }
}

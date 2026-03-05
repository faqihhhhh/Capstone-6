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
    public class DeleteTahunAkademikCommand : IRequest<ServiceResponse<string>>
    {
        public int Id { get; set; }
        public DeleteTahunAkademikCommand(int id)
        {
            Id = id;
        }

    }
}

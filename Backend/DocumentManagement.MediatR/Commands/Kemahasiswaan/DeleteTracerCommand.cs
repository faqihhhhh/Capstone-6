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
    public class DeleteTracerCommand : IRequest<ServiceResponse<string>>
    {
        public int StatusId {  get; set; }
        public DeleteTracerCommand(int statusId)
        {
            StatusId = statusId;
        }   
    }
}
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
    public class UpdateTracerCommand: IRequest<ServiceResponse<StatusLulusanUpdateDTO>>
    {
        public int StatusId { get; set; }
        public StatusLulusanUpdateDTO StatusDto { get; set; }

        public UpdateTracerCommand(int statusId, StatusLulusanUpdateDTO statusDto)
        {
            StatusId = statusId;
            StatusDto = statusDto;
        }
    }
}
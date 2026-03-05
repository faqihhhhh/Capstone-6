using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace DocumentManagement.MediatR.Commands
{
    public class DeletePegawaiCommand : IRequest<ServiceResponse<string>>
    {
        public Guid Id { get; set; }
        public DeletePegawaiCommand(Guid id)
        {
            Id = id;
        }
    }
}
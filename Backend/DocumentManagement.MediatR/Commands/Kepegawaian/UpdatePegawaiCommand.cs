using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace DocumentManagement.MediatR.Commands
{
    public class UpdatePegawaiCommand : IRequest<ServiceResponse<PegawaiDTO>>
    {
        public Guid Id { get; set; } 
        public PegawaiDTO PegawaiDto { get; set; }

        public UpdatePegawaiCommand(Guid id, PegawaiDTO pegawaiDto)
        {
            Id = id;
            PegawaiDto = pegawaiDto;
        }
    }
}

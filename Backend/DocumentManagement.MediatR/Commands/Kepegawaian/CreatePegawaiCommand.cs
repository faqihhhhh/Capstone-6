using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace DocumentManagement.MediatR.Commands
{
    public class CreatePegawaiCommand : IRequest<ServiceResponse<PegawaiDTO>>
    {
        public PegawaiDTO PegawaiDto { get; set; } // Ganti nama Pegawai menjadi PegawaiDto

        public CreatePegawaiCommand(PegawaiDTO pegawaiDto)
        {
            PegawaiDto = pegawaiDto;
        }
    }
}

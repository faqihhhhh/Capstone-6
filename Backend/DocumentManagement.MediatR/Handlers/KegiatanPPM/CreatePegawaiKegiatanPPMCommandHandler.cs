using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class CreatePegawaiKegiatanPPMCommandHandler : IRequestHandler<CreatePegawaiKegiatanPPMCommand, ServiceResponse<CreatePegawaiKegiatanPPMDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public CreatePegawaiKegiatanPPMCommandHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<CreatePegawaiKegiatanPPMDTO>> Handle(CreatePegawaiKegiatanPPMCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddPegawaiKegiatanPPMAsync(request.PegawaiKegiatanPPMDTO);
        }
    }
}
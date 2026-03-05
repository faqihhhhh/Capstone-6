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
    public class CreateKegiatanPPMCommandHandler : IRequestHandler<CreateKegiatanPPMCommand, ServiceResponse<CreateKegiatanPPMDTO>>
    {
        private readonly IKegiatanPPMRepository _kegiatanPPMRepository;

        public CreateKegiatanPPMCommandHandler(IKegiatanPPMRepository kegiatanPPMRepository)
        {
            _kegiatanPPMRepository = kegiatanPPMRepository;
        }

        public async Task<ServiceResponse<CreateKegiatanPPMDTO>> Handle(CreateKegiatanPPMCommand request, CancellationToken cancellationToken)
        {
            return await _kegiatanPPMRepository.AddKegiatanPPMAsync(request.KegiatanPPMDTO);
        }
    }
}
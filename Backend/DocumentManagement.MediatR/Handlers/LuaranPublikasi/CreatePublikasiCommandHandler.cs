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
    public class CreatePublikasiCommandHandler : IRequestHandler<CreatePublikasiCommand, ServiceResponse<CreatePublikasiDTO>>
    {
        private readonly IPublikasiRepository _publikasiRepository;

        public CreatePublikasiCommandHandler(IPublikasiRepository publikasiRepository)
        {
            _publikasiRepository = publikasiRepository;
        }

        public async Task<ServiceResponse<CreatePublikasiDTO>> Handle(CreatePublikasiCommand request, CancellationToken cancellationToken)
        {
            // Panggil method repository untuk menambahkan publikasi
            var result = await _publikasiRepository.AddPublikasiAsync(request.PublikasiDTO);

            return result;
        }
    }
}

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
    public class CreatePrestasiCommandHandler : IRequestHandler<CreatePrestasiCommand, ServiceResponse<CreatePrestasiDTO>>
    {
        private readonly IPrestasiRepository _tahunAkademikRepo;

        public CreatePrestasiCommandHandler(IPrestasiRepository tahunAkademikRepo)
        {
            _tahunAkademikRepo = tahunAkademikRepo;
        }

        public async Task<ServiceResponse<CreatePrestasiDTO>> Handle(CreatePrestasiCommand request, CancellationToken cancellationToken)
        {
            return await _tahunAkademikRepo.AddTahunAkademikAsync(request.TahunAkademikDto);
        }
    }
}

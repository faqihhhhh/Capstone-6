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
    public class GetPrestasiByIdQueryHandler : IRequestHandler<GetPrestasiByIdQuery, GetAllPrestasiDTO>
    {
        private readonly IPrestasiRepository _tahunAkademikRepo;

        public GetPrestasiByIdQueryHandler(IPrestasiRepository tahunAkademikRepo)
        {
            _tahunAkademikRepo = tahunAkademikRepo;
        }

        public async Task<GetAllPrestasiDTO> Handle(GetPrestasiByIdQuery request, CancellationToken cancellationToken)
        {
            return await _tahunAkademikRepo.GetTahunAkademikByIdAsync(request.Id);
        }
    }
}
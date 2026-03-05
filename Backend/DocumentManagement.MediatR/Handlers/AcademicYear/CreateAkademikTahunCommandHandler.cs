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
    public class CreateAkademikTahunCommandHandler : IRequestHandler<CreateAkademikTahunCommand, ServiceResponse<CreateTahunAkademikDTO>>
    {
        private readonly IAkademikTahunRepository _tahunAkademikRepo;

        public CreateAkademikTahunCommandHandler(IAkademikTahunRepository tahunAkademikRepo)
        {
            _tahunAkademikRepo = tahunAkademikRepo;
        }
        public async Task<ServiceResponse<CreateTahunAkademikDTO>> Handle(CreateAkademikTahunCommand request, CancellationToken cancellationToken)
        { 
            return await _tahunAkademikRepo.AddAkademikTahunAsync(request.TahunAkademikDTO);
        }
    }

}

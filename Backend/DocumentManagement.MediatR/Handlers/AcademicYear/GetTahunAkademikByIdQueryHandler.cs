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
    public class GetTahunAkademikByIdQueryHandler: IRequestHandler<GetTahunAkademikByIdQuery, GetTahunAkademikDTO>
    {
            private readonly IAkademikTahunRepository _tahunAkademikRepo;

            public GetTahunAkademikByIdQueryHandler(IAkademikTahunRepository tahunAkademikRepo)
            {   
                _tahunAkademikRepo = tahunAkademikRepo;
            }

            public async Task<GetTahunAkademikDTO> Handle(GetTahunAkademikByIdQuery request, CancellationToken cancellationToken)
            {
                return await _tahunAkademikRepo.GetTahunAkademikByIdAsync(request.Id);
            }
    }
}


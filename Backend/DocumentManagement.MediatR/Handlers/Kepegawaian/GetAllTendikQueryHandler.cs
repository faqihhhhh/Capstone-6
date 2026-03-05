using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class GetAllTendikQueryHandler : IRequestHandler<GetAllTendikQuery, IEnumerable<TendikDetailDTO>>
    {
        private readonly IPegawaiRepository _pegawaiRepository;

        public GetAllTendikQueryHandler(IPegawaiRepository pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }

        public async Task<IEnumerable<TendikDetailDTO>> Handle(GetAllTendikQuery request, CancellationToken cancellationToken)
        {
            return await _pegawaiRepository.GetAllTendikAsync();
        }
    }
}
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
    public class GetTendikByNIPQueryHandler : IRequestHandler<GetTendikByNIPQuery, TendikDetailDTO>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        public GetTendikByNIPQueryHandler(IPegawaiRepository pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }
        public async Task<TendikDetailDTO> Handle(GetTendikByNIPQuery request, CancellationToken cancellationToken)
        {
            return await _pegawaiRepository.GetTendikByNIPAsync(request.NIP);
        }
    }

}
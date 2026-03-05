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
    public class GetDosenByNIPQueryHandler : IRequestHandler<GetDosenByNIPQuery, DosenDetailDTO>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        public GetDosenByNIPQueryHandler(IPegawaiRepository pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }
        public async Task<DosenDetailDTO> Handle(GetDosenByNIPQuery request, CancellationToken cancellationToken)
        {
            return await _pegawaiRepository.GetDosenByNIPAsync(request.NIP);
        }
    }
}
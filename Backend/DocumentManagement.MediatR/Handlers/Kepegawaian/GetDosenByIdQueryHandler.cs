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
    public class GetDosenByIdQueryHandler : IRequestHandler<GetDosenByIdQuery, DosenDetailDTO>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        public GetDosenByIdQueryHandler(IPegawaiRepository pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }
        public async Task<DosenDetailDTO> Handle(GetDosenByIdQuery request, CancellationToken cancellationToken)
        {
            return await _pegawaiRepository.GetDosenByIdAsync(request.Id);
        }
    }
}
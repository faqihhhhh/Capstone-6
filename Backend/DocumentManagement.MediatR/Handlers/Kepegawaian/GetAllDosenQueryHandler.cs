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
    public class GetAllDosenQueryHandler : IRequestHandler<GetAllDosenQuery, IEnumerable<DosenDetailDTO>>
    {
        private readonly IPegawaiRepository _pegawaiRepository;

        public GetAllDosenQueryHandler(IPegawaiRepository pegawaiRepository)
        {
            _pegawaiRepository = pegawaiRepository;
        }

        public async Task<IEnumerable<DosenDetailDTO>> Handle(GetAllDosenQuery request, CancellationToken cancellationToken)
        {
            return await _pegawaiRepository.GetAllDosenAsync();
        }
    }

}

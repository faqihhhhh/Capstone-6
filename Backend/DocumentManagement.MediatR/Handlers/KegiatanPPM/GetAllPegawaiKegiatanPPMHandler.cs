using DocumentManagement.Data.Dto;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class GetAllPegawaiKegiatanPPMHandler : IRequestHandler<GetAllPegawaiKegiatanPPMQuery, IEnumerable<PegawaiKegiatanPPMResponseDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public GetAllPegawaiKegiatanPPMHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PegawaiKegiatanPPMResponseDTO>> Handle(GetAllPegawaiKegiatanPPMQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPegawaiKegiatanPPMAsync();
        }
    }
}

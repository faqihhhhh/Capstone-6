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
    public class GetAllKegiatanPPMHandler : IRequestHandler<GetAllKegiatanPPMQuery, List<KegiatanPPMListDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public GetAllKegiatanPPMHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<KegiatanPPMListDTO>> Handle(GetAllKegiatanPPMQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllKegiatanPPMAsync();
        }
    }
}

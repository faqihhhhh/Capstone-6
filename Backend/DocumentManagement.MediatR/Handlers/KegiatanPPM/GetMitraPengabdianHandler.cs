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
    public class GetMitraPengabdianHandler : IRequestHandler<GetMitraPengabdianQuery, List<MitraPengabdianDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public GetMitraPengabdianHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MitraPengabdianDTO>> Handle(GetMitraPengabdianQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMitraPengabdianAsync();
        }
    }
}

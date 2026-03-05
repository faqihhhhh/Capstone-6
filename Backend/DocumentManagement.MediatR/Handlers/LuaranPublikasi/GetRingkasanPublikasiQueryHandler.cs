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
    public class GetRingkasanPublikasiQueryHandler : IRequestHandler<GetRingkasanPublikasiQuery, IEnumerable<RingkasanPublikasiDTO>>
    {
        private readonly IPublikasiRepository _publikasiRepository;

        public GetRingkasanPublikasiQueryHandler(IPublikasiRepository publikasiRepository)
        {
            _publikasiRepository = publikasiRepository;
        }

        public async Task<IEnumerable<RingkasanPublikasiDTO>> Handle(GetRingkasanPublikasiQuery request, CancellationToken cancellationToken)
        {
            return await _publikasiRepository.GetPublikasiAggregatedAsync();
        }
    }
}

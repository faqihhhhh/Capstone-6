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
    public class GetRingkasanPengabdianPenelitianHandler : IRequestHandler<GetRingkasanPengabdianPenelitianQuery, List<RingkasanPengabdianPenelitianDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public GetRingkasanPengabdianPenelitianHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RingkasanPengabdianPenelitianDTO>> Handle(GetRingkasanPengabdianPenelitianQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetRingkasanPengabdianPenelitianAsync();
        }
    }
}

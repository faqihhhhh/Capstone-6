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
    public class GetAllTracerHandler : IRequestHandler<GetAllTracerQuery, IEnumerable<GetAllTahunLulusDTO>>
    {
        private readonly ITahunLulusRepository _tahunLulusRepository;

        public GetAllTracerHandler(ITahunLulusRepository tahunLulusRepository)
        {
            _tahunLulusRepository = tahunLulusRepository;
        }

        public async Task<IEnumerable<GetAllTahunLulusDTO>> Handle(GetAllTracerQuery request, CancellationToken cancellationToken)
        {
            // Ambil semua data tanpa filter LulusanTahun
            return await _tahunLulusRepository.GetAllTahunLulusWithStatusAsync();
        }
    }
}

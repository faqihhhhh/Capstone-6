using DocumentManagement.Data.Dto;
using MediatR;
using System;

namespace DocumentManagement.MediatR.Queries
{
    public class GetTendikByNIPQuery : IRequest<TendikDetailDTO>
    {
        public string NIP { get; set; }

        public GetTendikByNIPQuery(string nip)
        {
            NIP = nip;
        }
    }
}
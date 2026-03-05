using DocumentManagement.Data.Dto;
using MediatR;
using System;

namespace DocumentManagement.MediatR.Queries
{
    public class GetTendikByIdQuery : IRequest<TendikDetailDTO>
    {
        public Guid Id { get; set; }

        public GetTendikByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}


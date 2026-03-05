using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Commands
{
    public class AddDocumentCommentCommand : IRequest<ServiceResponse<DocumentCommentDto>>
    {
        public Guid DocumentId { get; set; }
        public string Comment { get; set; }
    }
}

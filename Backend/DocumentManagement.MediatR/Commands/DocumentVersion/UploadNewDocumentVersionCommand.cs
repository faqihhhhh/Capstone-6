using DocumentManagement.Data;
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
    public class UploadNewDocumentVersionCommand : IRequest<ServiceResponse<DocumentVersionDto>>
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Url { get; set; }
    }
}

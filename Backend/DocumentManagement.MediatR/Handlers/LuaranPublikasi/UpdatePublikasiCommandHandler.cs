using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class UpdatePublikasiCommandHandler : IRequestHandler<UpdatePublikasiCommand, UpdatePublikasiDTO>
    {
        private readonly IPublikasiRepository _publikasiRepository;

        public UpdatePublikasiCommandHandler(IPublikasiRepository publikasiRepository)
        {
            _publikasiRepository = publikasiRepository;
        }

        public async Task<UpdatePublikasiDTO> Handle(UpdatePublikasiCommand request, CancellationToken cancellationToken)
        {
            // Panggil method repository untuk memperbarui publikasi
            var result = await _publikasiRepository.UpdatePublikasiAsync(request.PublikasiId, request.PublikasiDTO);

            if (result == null)
            {
                throw new Exception("Publikasi tidak ditemukan."); // Jika ID tidak ditemukan
            }

            return result;
        }
    }
}

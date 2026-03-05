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
    public class CreateTahunLulusHandler : IRequestHandler<CreateTahunLulusCommand, int>
    {
        private readonly KemahasiswaanService _kemahasiswaanService;
        public CreateTahunLulusHandler(KemahasiswaanService kemahasiswaanService)
        {
            _kemahasiswaanService = kemahasiswaanService;

        }
        public async Task<int> Handle(CreateTahunLulusCommand request, CancellationToken cancellationToken)
        {
            //var tahunlulusDto = request.TahunLulusDto;
            //if (tahunlulusDto.LulusanTahun == null)
            //{
               // throw new ValidationException($"Tahun Lulus ({tahunLulusDTO.LulusanTahun}) .");
                //return ServiceResponse<TahunLulusWithTracerDTO>.ReturnFailed(400, "Lulusan Tahun tidak boleh kosong!.");
           // }
           return await _kemahasiswaanService.CreateTahunLulusWithTracerAsync(request.TahunLulusDto);

            //return ServiceResponse<TahunLulusWithTracerDTO>.ReturnSuccess();
        }
    }
}

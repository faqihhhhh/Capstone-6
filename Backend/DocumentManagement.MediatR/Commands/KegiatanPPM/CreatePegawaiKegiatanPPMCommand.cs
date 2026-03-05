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
    public class CreatePegawaiKegiatanPPMCommand : IRequest<ServiceResponse<CreatePegawaiKegiatanPPMDTO>>
    {
        public CreatePegawaiKegiatanPPMDTO PegawaiKegiatanPPMDTO { get; set; }
        public CreatePegawaiKegiatanPPMCommand(CreatePegawaiKegiatanPPMDTO pegawaiKegiatanPPMDTO)
        {
            PegawaiKegiatanPPMDTO = pegawaiKegiatanPPMDTO;
        }
    }
}

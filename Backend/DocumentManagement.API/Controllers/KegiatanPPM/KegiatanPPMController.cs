using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Domain;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KegiatanPPMController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DocumentContext _context;
        public KegiatanPPMController(DocumentContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("AddPPM")]
        public async Task<IActionResult> CreateKegiatanPPM([FromBody] CreateKegiatanPPMDTO dto)
        {
            var response = await _mediator.Send(new CreateKegiatanPPMCommand(dto));
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, new { message = response.Errors.FirstOrDefault() });
            }
            return StatusCode(response.StatusCode,
                new { message = "Data Kegiatan PPM berhasil ditambahkan", judulPPM = dto.JudulPPM, tahunMulai = dto.TahunMulai });
        }

        [HttpPost("AddRelation")]
        public async Task<IActionResult> CreatePegawaiKegiatanPPM([FromBody] CreatePegawaiKegiatanPPMDTO dto)
        {
            var response = await _mediator.Send(new CreatePegawaiKegiatanPPMCommand(dto));
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, new { message = response.Errors.FirstOrDefault() });
            }
            return StatusCode(response.StatusCode,
                new { message = "Data Kegiatan PPM berhasil ditambahkan", data = response.Data });
        }

        [HttpGet("cariPPM")]
        public async Task<IActionResult> GetKegiatanPPMByJudulAndTahun(string judulPPM, int tahunMulai)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(judulPPM) || tahunMulai <= 0)
            {
                return BadRequest("JudulPPM dan TahunMulai harus diisi.");
            }

            // Cari data berdasarkan JudulPPM dan TahunMulai
            var kegiatan = await _context.KegiatanPPMs
                .Where(k => k.JudulPPM == judulPPM && k.TahunMulai == tahunMulai)
                .Select(k => new
                {
                    KegiatanId = k.KegiatanID
                })
                .FirstOrDefaultAsync();

            if (kegiatan == null)
            {
                return NotFound("Data tidak ditemukan.");
            }

            return Ok(kegiatan);
        }

        [HttpGet("GetAllRelations")]
        public async Task<IActionResult> GetAllPegawaiKegiatanPPM()
        {
            var result = await _mediator.Send(new GetAllPegawaiKegiatanPPMQuery());
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllKegiatanPPM()
        {
            var result = await _mediator.Send(new GetAllKegiatanPPMQuery());

            if (result == null || result.Count == 0)
            {
                return NotFound("Tidak ada data KegiatanPPM yang ditemukan.");
            }

            return Ok(result);
        }

        [HttpGet("RingkasanPengabdianPenelitian")]
        public async Task<IActionResult> GetRingkasanPengabdianPenelitian()
        {
            var result = await _mediator.Send(new GetRingkasanPengabdianPenelitianQuery());

            if (result == null || result.Count == 0)
            {
                return NotFound("Tidak ada data ringkasan Pengabdian dan Penelitian yang ditemukan.");
            }

            return Ok(result);
        }

        [HttpGet("MitraPenelitian")]
        public async Task<IActionResult> GetMitraPenelitian()
        {
            var result = await _mediator.Send(new GetMitraPenelitianQuery());

            if (result == null || result.Count == 0)
            {
                return NotFound("Tidak ada data Mitra Penelitian yang ditemukan.");
            }

            return Ok(result);
        }

        [HttpGet("MitraPengabdian")]
        public async Task<IActionResult> GetMitraPengabdian()
        {
            var result = await _mediator.Send(new GetMitraPengabdianQuery());

            if (result == null || result.Count == 0)
            {
                return NotFound("Tidak ada data Mitra Pengabdian yang ditemukan.");
            }

            return Ok(result);
        }
        [HttpDelete("/delete-ppm/{kegiatanId}")]
        public async Task<IActionResult> DeleteKegiatanPPM(Guid kegiatanId)
        {
            var result = await _mediator.Send(new DeleteKegiatanPPMCommand(kegiatanId));

            if (!result)
            {
                return NotFound($"Kegiatan PPM tidak ditemukan.");
            }

            return Ok(new { Message = $"Kegiatan PPM berhasil dihapus." });
        }
        [HttpPut("/update-ppm/{kegiatanId}")]
        public async Task<IActionResult> UpdateKegiatanPPM(Guid kegiatanId, [FromBody] UpdateKegiatanPPMDTO updateDto)
        {
            var result = await _mediator.Send(new UpdateKegiatanPPMCommand(kegiatanId, updateDto));

            if (!result)
            {
                return NotFound($"Kegiatan PPM {updateDto.JudulPPM} tidak ditemukan.");
            }

            return Ok(new { Message = $"Kegiatan PPM dengan {updateDto.JudulPPM} berhasil diperbarui." });
        }
    }
}

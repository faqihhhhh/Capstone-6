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
    public class LuaranPublikasiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LuaranPublikasiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddPublikasi")]
        public async Task<IActionResult> CreatePublikasi([FromBody] CreatePublikasiDTO publikasiDTO)
        {
            var response = await _mediator.Send(new CreatePublikasiCommand(publikasiDTO));
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, new { message = response.Errors.FirstOrDefault() });
            }
            return StatusCode(response.StatusCode,
                new { message = "Data Luaran Publikasi berhasil ditambahkan", data = response.Data });
        }
        // Endpoint untuk mendapatkan semua Publikasi
        [HttpGet("get-all-publikasi")]
        public async Task<IActionResult> GetAllPublikasi()
        {
            var result = await _mediator.Send(new GetAllPublikasiQuery());
            /*
             if (result == null || !result.Any())
            {
                return NotFound("Tidak ada data publikasi yang ditemukan.");
            }
             */
            return Ok(result);
        }
        // Endpoint untuk mendapatkan ringkasan publikasi
        [HttpGet("ringkasan-publikasi")]
        public async Task<IActionResult> GetRingkasanPublikasi()
        {
            var result = await _mediator.Send(new GetRingkasanPublikasiQuery());

            //if (result == null || !result.Any())
            //{
            //    return NotFound("Tidak ada data publikasi yang ditemukan.");
            //}

            return Ok(result);
        }
        // Endpoint untuk menghapus publikasi
        [HttpDelete("delete-publikasi/{publikasiId}")]
        public async Task<IActionResult> DeletePublikasi(Guid publikasiId)
        {
            var result = await _mediator.Send(new DeletePublikasiCommand(publikasiId));

            if (result.IsDeleted)
            {
                return Ok(result); // Publikasi berhasil dihapus
            }
            else
            {
                return NotFound(result); // Publikasi tidak ditemukan atau gagal dihapus
            }
        }
        [HttpPut("update-publikasi/{publikasiId}")]
        public async Task<IActionResult> UpdatePublikasi(Guid publikasiId, [FromBody] UpdatePublikasiDTO publikasiDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _mediator.Send(new UpdatePublikasiCommand(publikasiId, publikasiDTO));
                return Ok(result); // Publikasi berhasil diperbarui
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Publikasi tidak ditemukan
            }
        }
    }
}

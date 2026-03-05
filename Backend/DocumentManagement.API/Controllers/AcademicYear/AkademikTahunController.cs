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
    [ApiController]
    [Route("api/[controller]")]
    public class AkademikTahunController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AkademikTahunController(DocumentContext context, IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAkademikTahun([FromBody] CreateTahunAkademikDTO tahunAkademikDto)
        {
            var result = await _mediator.Send(new CreateAkademikTahunCommand(tahunAkademikDto));
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Errors.FirstOrDefault() });
            }
            //return Ok(result);
            return StatusCode(result.StatusCode, new { message = "Tahun Akademik berhasil ditambahkan", data = result.Data });
        }

        [HttpDelete("{thnAkademikId}")]
        public async Task<IActionResult> DeleteTahunAkademik(int thnAkademikId)
        {
            var result = await _mediator.Send(new DeleteTahunAkademikCommand(thnAkademikId));
            return Ok(result);
        }

        // Method GET untuk mendapatkan seluruh data TahunAkademik
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GetTahunAkademikDTO>>> GetAllTahunAkademik()
        {
            var result = await _mediator.Send(new GetAllTahunAkademikQuery());
            return Ok(result);
        }

        // Method GET untuk mendapatkan data TahunAkademik berdasarkan ID
        [HttpGet("by-id/{thnAkademikId}")]
        public async Task<IActionResult> GetTahunAkademikById(int thnAkademikId)
        {
            var result = await _mediator.Send(new GetTahunAkademikByIdQuery(thnAkademikId));
            if (result == null)
            {
                return NotFound("Tahun Akademik tidak ditemukan.");
            }
            return Ok(result);
        }
    }
}

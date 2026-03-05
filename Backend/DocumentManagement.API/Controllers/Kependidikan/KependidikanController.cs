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
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KependidikanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KependidikanController(DocumentContext context, IMediator mediator)
        {
            _mediator = mediator;
        }
        //============================================================//
        // Controller CREATE membuat Data Kependidikan (Metode POST)
        //============================================================//
        [HttpPost("kependidikan")]
        public async Task<IActionResult> CreateKependidikan([FromBody] CreateKependidikanDTO kependidikanDto)
        {
            var result = await _mediator.Send(new CreateKependidikanCommand(kependidikanDto));
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Errors.FirstOrDefault() });

            }
            return StatusCode(result.StatusCode, new { message = "Data Kependidikan berhasil ditambahkan", data = result.Data });
        }

        //============================================================//
        // Controller GET mengakses Data Kependidikan (Metode GET)
        //============================================================//
        //[AllowAnonymous]
        [HttpGet("kependidikan/all")]
        public async Task<ActionResult<IEnumerable<GetKependidikanDTO>>> GetDataKependidikan()
        {
            var query = new GetAllDataKependidikanQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("kependidikan/by-id/{thnAkademikId}")]
        public async Task<ActionResult<List<GetKependidikanDTO>>> GetDataKependidikanById(int thnAkademikId)
        {
            var result = await _mediator.Send(new GetDataKependidikanByIdQuery(thnAkademikId));

            if (result == null || result.Count == 0)
            {
                return NotFound($"Data untuk TahunAkademik dengan ID {thnAkademikId} tidak ditemukan.");
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDataKependidikan([FromBody] DeleteKependidikanDTO dto)
        {
            var command = new DeleteKependidikanCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDataKependidikan([FromBody] UpdateKependidikanDTO dto)
        {
            var command = new UpdateDataKependidikanCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return NotFound(result.StatusCode);
            }

            return Ok(result);
        }
    }
}
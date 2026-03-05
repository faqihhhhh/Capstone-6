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
    public class KemahasiswaanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KemahasiswaanController(DocumentContext context, IMediator mediator)
        {
            _mediator = mediator;
        }

        //====================================================================================================//
        // Controller untuk CREATE membuat Data Status Lulusan, Masa Tunggu dan lain-lain (Metode POST)
        //====================================================================================================//
        
        /// <summary>
        /// Metode Post untuk Create Status Lulusan (Kemahasiswaan)
        /// </summary>
        /// <param name="tahunLulusDTO"></param>
        /// <returns></returns>
        [HttpPost("tracer/lulusan")]
        public async Task<IActionResult> CreateTahunLulus([FromBody] TahunLulusWithTracerDTO tahunLulusDTO)
        {
            var result = await _mediator.Send(new CreateTahunLulusCommand(tahunLulusDTO));
            //return Ok(result);
            return Ok(new { TahunId = result});
        }

        //====================================================================================================//
        // Controller untuk CREATE Data Prestasi Mahasiswa (Metode POST)
        //====================================================================================================//
        
        /// <summary>
        /// Method Post untuk Data Prestasi berdasarkan Data Tahun Akademik
        /// </summary>
        /// <param name="tahunAkademikDto"></param>
        /// <returns></returns>
        [HttpPost("prestasi/mahasiswa")]
        public async Task<IActionResult> CreateTahunAkademik([FromBody] CreatePrestasiDTO tahunAkademikDto)
        {
            //if (!ModelState.IsValid) { 
            //    return BadRequest(ModelState);
            //}
            var result = await _mediator.Send(new CreatePrestasiCommand(tahunAkademikDto));
            if (!result.Success) { 
                return StatusCode(result.StatusCode, new { message = result.Errors.FirstOrDefault()});
            }
            //return Ok(result);
            return StatusCode(result.StatusCode, new { message = "Tahun Akademik berhasil ditambahkan", data = result.Data });
        }

        /// <summary>
        /// GET ALL Data Tahun dan Status Lulusan
        /// </summary>
        /// <returns></returns>
        [HttpGet("tracer/all")]
        public async Task<ActionResult<IEnumerable<GetAllTahunLulusDTO>>> GetAllTahunLulusWithStatus()
        {
            // Kirim query ke MediatR untuk mengambil semua data
            var result = await _mediator.Send(new GetAllTracerQuery());
            return Ok(result);
        }

        [HttpGet("prestasi/all")]
        public async Task<ActionResult<IEnumerable<GetAllPrestasiDTO>>> GetAllPrestasi()
        {
            var result = await _mediator.Send(new GetAllPrestasiQuery());
            return Ok(result);
        }

        // Method GET untuk mendapatkan data TahunAkademik berdasarkan ID
        [HttpGet("prestasi/by-id/{thnAkademikId}")]
        public async Task<IActionResult> GetPrestasiById(int thnAkademikId)
        {
            var result = await _mediator.Send(new GetPrestasiByIdQuery(thnAkademikId));
            if (result == null)
            {
                return NotFound("Tahun Akademik tidak ditemukan.");
            }
            return Ok(result);
        }

        [HttpDelete("tracer/{statusId}")]
        public async Task<IActionResult> DeleteTracer(int statusId)
        {
            // Kirimkan command ke MediatR untuk menghapus data
            var result = await _mediator.Send(new DeleteTracerCommand(statusId));
            return Ok(result);
        }

        [HttpDelete("prestasi/{thnAkademikId}")]
        public async Task<IActionResult> DeletePrestasiMhs(int thnAkademikId)
        {
            var result = await _mediator.Send(new DeletePrestasiCommand(thnAkademikId));
            return Ok(result);
        }

        [HttpPut("tracer/{statusId}")]
        public async Task<IActionResult> UpdateStatusLulusan(int statusId, [FromBody] StatusLulusanUpdateDTO statusLulusanDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Kirimkan command ke MediatR untuk melakukan update
                var result = await _mediator.Send(new UpdateTracerCommand(statusId, statusLulusanDTO));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("prestasi/{thnAkademikId}")]
        public async Task<IActionResult> UpdatePrestasiMahasiswa(int thnAkademikId, [FromBody] UpdatePrestasiMhsDTO prestasiMhsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
              var result = await _mediator.Send(new UpdatePrestasiCommand(thnAkademikId, prestasiMhsDto));
              return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            // Kirimkan command ke MediatR untuk melakukan update
            //var result = await _mediator.Send(new UpdatePrestasiCommand(tahunAkademikDto));
            //return Ok(result);
        }
    }
}

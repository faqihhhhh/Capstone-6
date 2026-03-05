using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManagement.Data.Dto;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    /// <summary>
    /// Pada bagian ini MediatR akan digunakan untuk memanggil handler-handler yang sudah dibuat
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PegawaiController : ControllerBase
    {
        //public IMediator _mediator { get; set; }
        private readonly IMediator _mediator;
        /// <summary>
        /// Kepegawaian
        /// </summary>
        public PegawaiController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        //====================================================================================================//
        // Controller untuk Dosen --> menampilkan data Dosen (Metode GET)
        //====================================================================================================//
        /// <summary>
        /// Get All Dosen (Termasuk Pegawai dan Dosen Tetap)
        /// </summary>
        /// <returns></returns>
        [HttpGet("dosen")]
        [Produces("application/json", "application/xml", Type = typeof(List<DosenDetailDTO>))]
        public async Task<IActionResult> GetAllDosen()
        {
            var result = await _mediator.Send(new GetAllDosenQuery());
            return Ok(result);
        }
        /// <summary>
        /// Get Dosen By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("dosen/by-id/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(DosenDetailDTO))]
        public async Task<IActionResult> GetDosenById(Guid id)
        {
            var result = await _mediator.Send(new GetDosenByIdQuery(id));
            return Ok(result);
        }
        /// <summary>
        /// Get Dosen By NIP
        /// </summary>
        /// <param name="nip"></param>
        /// <returns></returns>
        [HttpGet("dosen/by-nip/{nip}")]
        [Produces("application/json", "application/xml", Type = typeof(DosenDetailDTO))]
        public async Task<IActionResult> GetDosenByNIP(string nip)
        {
            var result = await _mediator.Send(new GetDosenByNIPQuery(nip));
            return Ok(result);
        }
        
        //====================================================================================================//
        // Controller untuk GET Tendik --> menampilkan data Tendik (Metode GET)
        //====================================================================================================//
        /// <summary>
        /// Get All Tendik (Termasuk Pegawai dan Tendik Tetap)
        /// </summary>
        /// <returns></returns>
        [HttpGet("tendik")]
        [Produces("application/json", "application/xml", Type = typeof(List<TendikDetailDTO>))]
        public async Task<IActionResult> GetAllTendik()
        {
            var result = await _mediator.Send(new GetAllTendikQuery());
            return Ok(result);
        }
        
        /// <summary>
        /// Get Tendik By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tendik/by-id/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TendikDetailDTO))]
        public async Task<IActionResult> GetTendikById(Guid id)
        {
            var result = await _mediator.Send(new GetTendikByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("tendik/by-nip/{nip}")]
        [Produces("application/json", "application/xml", Type = typeof(TendikDetailDTO))]
        public async Task<IActionResult> GetTendikByBIP(String nip)
        {
            var result = await _mediator.Send(new GetTendikByNIPQuery(nip));
            return Ok(result);
        }

        //====================================================================================================//
        // Controller untuk CREATE membuat Data Pegawai, Dosen, Dosen Tetap, Tendik, Tendik Tetap (Metode POST)
        //====================================================================================================//
        
        /// <summary>
        /// Metode Post untuk Create Pegawai
        /// </summary>
        /// <param name="pegawaiDto"></param>
        /// <returns></returns>
        [HttpPost]
        //[Produces("application/json", "application/xml", Type = typeof(PegawaiDTO))]
        public async Task<IActionResult> CreatePegawai([FromBody] PegawaiDTO pegawaiDto)
        {
            var result = await _mediator.Send(new CreatePegawaiCommand(pegawaiDto));
            return Ok(result);
        }
        
        //====================================================================================================//
        // Controller untuk UPDATE Data Pegawai, Dosen, Dosen Tetap, Tendik, Tendik Tetap (Metode PUT)
        //====================================================================================================//
        
        /// <summary>
        /// Metode Update (PUT)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pegawaiDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PegawaiDTO))]
        public async Task<IActionResult> UpdatePegawai(Guid id, [FromBody] PegawaiDTO pegawaiDto)
        {
            var result = await _mediator.Send(new UpdatePegawaiCommand(id, pegawaiDto));
            return Ok(result);
        }

        //====================================================================================================//
        // Controller untuk DELETE Data Pegawai, Dosen, Dosen Tetap, Tendik, Tendik Tetap (Metode Delete)
        //====================================================================================================//
        /// <summary>
        /// Delete Pegawai By NIP
        /// </summary>
        /// <param name="nip"></param>
        /// <returns></returns>
        [HttpDelete("by-nip/{nip}")]
        public async Task<IActionResult> DeletePegawaiByNIP(string nip) 
        {
            var result = await _mediator.Send(new DeletePegawaiByNIPCommand(nip));
            return Ok(result);
        }
        [HttpDelete("by-id/{id}")]
        public async Task<IActionResult> DeletePegawaiById(Guid id)
        {
            var result = await _mediator.Send(new DeletePegawaiCommand(id));
            return Ok(result);
        }
    }
}
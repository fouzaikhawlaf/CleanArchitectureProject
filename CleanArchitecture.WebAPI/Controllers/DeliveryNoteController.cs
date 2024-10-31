using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.FrameworkAndDrivers.Exceptions;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryNoteController : ControllerBase
    {
        private readonly IDeliveryNoteService _deliveryNoteService;

        public DeliveryNoteController(IDeliveryNoteService deliveryNoteService)
        {
            _deliveryNoteService = deliveryNoteService;
        }

        // GET: api/deliverynote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryNoteDto>>> GetAll()
        {
            var deliveryNotes = await _deliveryNoteService.GetAllAsync();
            return Ok(deliveryNotes);
        }

        // GET: api/deliverynote/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryNoteDto>> GetById(int id)
        {
            var deliveryNote = await _deliveryNoteService.GetByIdAsync(id);
            if (deliveryNote == null)
                return NotFound();

            return Ok(deliveryNote);
        }


       
        // PUT: api/deliverynote/{id:int}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DeliveryNoteDto>> UpdateDeliveryNote([FromRoute] int id, [FromBody] UpdateDeliveryNoteDto deliveryNoteDto)
        {
            try
            {
                // Vérifie si le modèle est valide
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Récupère le bon de livraison par ID
                var deliveryNoteToUpdate = await _deliveryNoteService.GetByIdAsync(id);
                if (deliveryNoteToUpdate == null)
                {
                    return NotFound($"Delivery note with Id = {id} not found");
                }

                // Met à jour le bon de livraison
                await _deliveryNoteService.UpdateAsync(id, deliveryNoteDto);
                return Ok(deliveryNoteDto); // Retourne le DTO mis à jour
            }
            catch (InvalidEntityException ex)
            {
                // Log de l'exception si nécessaire
                return BadRequest(ex.Message);
            }
            catch (Exception ) // Log des autres exceptions
            {
                // Log de l'exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }




        // DELETE: api/deliverynote/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _deliveryNoteService.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/deliverynote/createfromorder/{orderClientId}
        [HttpPost("createfromorder/{orderClientId}")]
        public async Task<ActionResult<DeliveryNoteDto>> CreateFromOrder(int orderClientId, [FromBody] DateTime deliveryDate)
        {
            var deliveryNote = await _deliveryNoteService.CreateFromOrderAsync(orderClientId, deliveryDate);
            return CreatedAtAction(nameof(GetById), new { id = deliveryNote.Id }, deliveryNote);
        }

        // PUT: api/deliverynote/markasdelivered/{orderClientId}
        [HttpPut("markasdelivered/{orderClientId}")]
        public async Task<ActionResult> MarkAsDelivered(int orderClientId)
        {
            await _deliveryNoteService.MarkOrderAsDeliveredAsync(orderClientId);
            return NoContent();
        }

        // GET: api/deliverynote/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DeliveryNoteDto>>> Search([FromQuery] int? orderClientId, [FromQuery] DateTime? deliveryDate)
        {
            var deliveryNotes = await _deliveryNoteService.SearchDeliveryNotesAsync(orderClientId, deliveryDate);
            return Ok(deliveryNotes);
        }

        // GET: api/deliverynote/archived
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<DeliveryNoteDto>>> GetArchived()
        {
            var archivedNotes = await _deliveryNoteService.GetArchivedDeliveryNotesAsync();
            return Ok(archivedNotes);
        }

        // PUT: api/deliverynote/markasarchived/{id}
        [HttpPut("markasarchived/{id}")]
        public async Task<ActionResult> MarkAsArchived(int id)
        {
            await _deliveryNoteService.MarkAsArchivedAsync(id);
            return NoContent();
        }


        // GET: api/deliverynote/pdf/{id:int}
        [HttpGet("pdf/{id:int}")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            try
            {
                var pdfContent = await _deliveryNoteService.GeneratePdfAsync(id);
                var fileName = $"DeliveryNote_{id}.pdf";
                return File(pdfContent, "application/pdf", fileName);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error generating PDF");
            }
        }

    }
}

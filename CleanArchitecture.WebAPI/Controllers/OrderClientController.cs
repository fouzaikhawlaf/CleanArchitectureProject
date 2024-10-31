using CleanArchitecture.UseCases.Dtos.OrderDtos;
using CleanArchitecture.UseCases.Dtos.SalesDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderClientController : ControllerBase
    {
        private readonly IOrderClientService _orderClientService;

        public OrderClientController(IOrderClientService orderClientService)
        {
            _orderClientService = orderClientService;
        }

        // GET: api/OrderClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderClientDto>>> GetAll()
        {
            try
            {
                var orders = await _orderClientService.GetAllAsync();
                return Ok(orders);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while retrieving orders.");
            }
        }

        // GET: api/OrderClient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderClientDto>> GetById(int id)
        {
            try
            {
                var order = await _orderClientService.GetByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while retrieving the order.");
            }
        }

        // POST: api/OrderClient
        [HttpPost]
        public async Task<ActionResult<OrderClientDto>> Create([FromBody] CreateOrderClientDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                var createdOrder = await _orderClientService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        // PUT: api/OrderClient/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderClientDto updateOrderDto)
        {
            if (updateOrderDto == null)
            {
                return BadRequest("Order update data is required.");
            }

            try
            {
                await _orderClientService.UpdateAsync(id, updateOrderDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while updating the order.");
            }
        }

        // DELETE: api/OrderClient/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderClientService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }

        // PATCH: api/OrderClient/{id}/archive
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                await _orderClientService.ArchiveOrderAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while archiving the order.");
            }
        }

        // GET: api/OrderClient/search?keyword={keyword}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<OrderClientDto>>> Search([FromQuery] string keyword)
        {
            try
            {
                var orders = await _orderClientService.SearchAsync(keyword);
                return Ok(orders);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while searching for orders.");
            }
        }

        // GET: api/OrderClient/{id}/total
        [HttpGet("{id}/total")]
        public async Task<ActionResult<double>> GetOrderTotal(int id)
        {
            try
            {
                var order = await _orderClientService.GetByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                var total = _orderClientService.CalculateTotalAmount(order);
                return Ok(total);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while calculating the order total.");
            }
        }

        // PATCH: api/OrderClient/{id}/confirm
        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            try
            {
                await _orderClientService.ConfirmOrderAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                return StatusCode(500, "An error occurred while confirming the order.");
            }
        }

        // PATCH: api/OrderClient/{id}/validate
        [HttpPatch("{id}/validate")]
        public async Task<ActionResult<OrderClientDto>> ValidateOrder(int id)
        {
            try
            {
                var validatedOrder = await _orderClientService.ValidateOrder(id);
                return Ok(validatedOrder);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception )
            {
                return StatusCode(500, "An error occurred while validating the order.");
            }
        }

        // GET: api/OrderClient/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<OrderClientDto>>> GetOrdersByClientId(int clientId)
        {
            try
            {
                var orders = await _orderClientService.GetOrdersByClientIdAsync(clientId);
                return Ok(orders);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while retrieving orders for the client.");
            }
        }

        // GET: api/OrderClient/pending
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<OrderClientDto>>> GetPendingOrders()
        {
            try
            {
                var orders = await _orderClientService.GetPendingOrdersAsync();
                return Ok(orders);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while retrieving pending orders.");
            }
        }

        // GET: api/OrderClient/archived
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<OrderClientDto>>> GetArchivedOrders()
        {
            try
            {
                var orders = await _orderClientService.GetArchivedOrdersAsync();
                return Ok(orders);
            }
            catch (Exception )
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while retrieving archived orders.");
            }
        }


        [HttpGet("{orderId}/pdf")]
        public async Task<IActionResult> GenerateOrderPdf(int orderId)
        {
            try
            {
                var pdfBytes = await _orderClientService.GenerateOrderPdfAsync(orderId);

                if (pdfBytes == null)
                {
                    return NotFound();
                }

                // Retourner le fichier PDF avec le bon content-type
                return File(pdfBytes, "application/pdf", $"Order_{orderId}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


    }
}

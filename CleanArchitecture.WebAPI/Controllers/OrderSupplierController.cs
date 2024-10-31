using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Enum;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSupplierController : ControllerBase
    {
        private readonly IOrderSupplierService _orderSupplierService;

        public OrderSupplierController(IOrderSupplierService orderSupplierService)
        {
            _orderSupplierService = orderSupplierService;
        }

        // POST: api/ordersupplier
        [HttpPost]
        public async Task<ActionResult<OrderSupplierDto>> CreateOrderSupplier([FromBody] CreateOrderSupplierDto dto)
        {
            var orderSupplierDto = await _orderSupplierService.CreateOrderSupplierAsync(dto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = orderSupplierDto.Id }, orderSupplierDto);
        }

        // GET: api/ordersupplier/{id}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderSupplierDto>> GetOrderById(int orderId)
        {
            var order = await _orderSupplierService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // GET: api/ordersupplier/supplier/{supplierId}
        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<OrderSupplierDto>>> GetOrdersBySupplier(int supplierId)
        {
            var orders = await _orderSupplierService.GetOrdersBySupplierAsync(supplierId);
            return Ok(orders);
        }

        // GET: api/ordersupplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderSupplierDto>>> GetAllOrders()
        {
            var orders = await _orderSupplierService.GetArchivedOrdersAsync();
            return Ok(orders);
        }

        // PUT: api/ordersupplier/{orderId}/approve
        [HttpPut("{orderId}/approve")]
        public async Task<ActionResult<OrderSupplierDto>> ApproveOrder(int orderId)
        {
            var approvedOrder = await _orderSupplierService.ApproveOrderAsync(orderId);
            return Ok(approvedOrder);
        }

        // PUT: api/ordersupplier/{orderId}/reject
        [HttpPut("{orderId}/reject")]
        public async Task<ActionResult> RejectOrder(int orderId)
        {
            await _orderSupplierService.RejectOrderAsync(orderId);
            return NoContent();
        }

        // GET: api/ordersupplier/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderSupplierDto>>> GetOrdersByStatus(OrderState status)
        {
            var orders = await _orderSupplierService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        // POST: api/ordersupplier/pdf
        [HttpPost("pdf")]
        public async Task<ActionResult<byte[]>> GenerateOrderPdf([FromBody] IEnumerable<int> orderIds)
        {
            var pdfBytes = await _orderSupplierService.GenerateOrderPdfAsync(orderIds);
            return File(pdfBytes, "application/pdf", "orders.pdf");
        }

        // Other actions (e.g., archive, unarchive, etc.) can be added here
    }
}

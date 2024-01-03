using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Application.Models.BookOrder.Command.RegisterBookOrder;
using PGB.Application.Models.BookOrder.Query;

namespace PGB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookOrderController : ControllerBase
    {
        private readonly ISender _mediator;
        public BookOrderController(ISender mediator) =>_mediator = mediator;
        

        [HttpPost("RegisterBookOrder")]
        public async Task<IActionResult> RegisterBookOrder(RegisterBookOrderCmd cmd)
        {
            var bookOrder = await _mediator.Send(cmd);
            return Ok(bookOrder);
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id) 
        {
            var qeury = new GetAllOrdersQuery()
            {
                Id = id,
            };
            var order = await _mediator.Send(qeury);
            return Ok(order);
        }

        [HttpGet("FindLastOrder")]
        public async Task<ActionResult<OrderDTO>> FindLastOrder(int userId)
        {
            var qeury = new FindLastOrderQuery()
            {
                userId = userId,
            };
            var order = await _mediator.Send(qeury);
            return Ok(order);
        }

    

    }
}

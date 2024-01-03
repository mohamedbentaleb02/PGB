using MediatR;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.BookOrder.Query
{
    public class FindLastOrderQueryHandler : IRequestHandler<FindLastOrderQuery, OrderDTO>
    {
        private readonly IBookOrderService _service;
        public FindLastOrderQueryHandler(IBookOrderService service) => _service = service;
        
        public async Task<OrderDTO> Handle(FindLastOrderQuery request, CancellationToken cancellationToken)
        {
            return await _service.FindLastOrder(request.userId);
        }
    }
}

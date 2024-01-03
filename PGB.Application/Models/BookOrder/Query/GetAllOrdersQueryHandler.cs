using MediatR;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Application.Interfaces;
using PGB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.BookOrder.Query
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, OrderDTO>
    {

        private readonly IBookOrderService _service;
        public GetAllOrdersQueryHandler(IBookOrderService service) => _service = service;
        public async Task<OrderDTO> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetOrderById(request.Id);
        }
    }
}

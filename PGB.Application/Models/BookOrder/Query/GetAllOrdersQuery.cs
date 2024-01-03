using MediatR;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Domain.Entities;

namespace PGB.Application.Models.BookOrder.Query
{
    public class GetAllOrdersQuery : IRequest<OrderDTO>
    {
        public int Id { get; set; }
    }
}

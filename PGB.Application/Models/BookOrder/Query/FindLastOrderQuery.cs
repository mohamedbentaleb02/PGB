using MediatR;
using PGB.Application.DTOs.BookOrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.BookOrder.Query
{
    public class FindLastOrderQuery : IRequest<OrderDTO>
    {
        public int userId { get; set; }
    }
}

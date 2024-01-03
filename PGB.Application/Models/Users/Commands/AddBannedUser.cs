using MediatR;
using PGB.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Commands
{
    public class AddBannedUser : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}

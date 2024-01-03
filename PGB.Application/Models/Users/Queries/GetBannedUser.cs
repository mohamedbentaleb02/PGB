using MediatR;
using PGB.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Queries
{
    public class GetBannedUser : IRequest<BannedUserDto>
    {
        public int id { get; set; }
    }
}

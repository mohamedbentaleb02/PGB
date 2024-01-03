using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Commands
{
    public class RemoveBannedUser : IRequest<bool>
    {
        public int userId { get; set; }
    }
}

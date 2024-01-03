using MediatR;
using PGB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Queries
{
    public class GetAllBannedUsers : IRequest<IEnumerable<BannedUser>>
    {
    }
}

using MediatR;
using PGB.Application.DTOs.Users;
using PGB.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Queries
{
    public class GetBannedUserHandler : IRequestHandler<GetBannedUser, BannedUserDto>
    {
        private readonly IService _service;
        public GetBannedUserHandler(IService service) => _service = service;
        
        public async Task<BannedUserDto> Handle(GetBannedUser request, CancellationToken cancellationToken)
        {
            return await _service.GetBannedUser(request.id);
        }
    }
}

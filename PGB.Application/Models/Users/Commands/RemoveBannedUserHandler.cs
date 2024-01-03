using MediatR;
using PGB.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGB.Application.Models.Users.Commands
{
    public class RemoveBannedUserHandler : IRequestHandler<RemoveBannedUser, bool>
    {
        private readonly IService _service;

        public RemoveBannedUserHandler(IService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(RemoveBannedUser request, CancellationToken cancellationToken)
        {
            return await _service.UnbanUser(request.userId);
        }
    }
}

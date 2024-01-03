using MediatR;
using PGB.Application.Interfaces;
using PGB.Domain.Entities;


namespace PGB.Application.Models.Users.Queries
{
    public class GetAllBannedUsersHandler : IRequestHandler<GetAllBannedUsers, IEnumerable<BannedUser>>
    {
        private readonly IService _service;
        public GetAllBannedUsersHandler(IService service) => _service = service;
        
        public async Task<IEnumerable<BannedUser>> Handle(GetAllBannedUsers request, CancellationToken cancellationToken)
        {
            return await _service.GetAllBannedUsers();
        }
    }
}

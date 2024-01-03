using MediatR;
using PGB.Application.DTOs.Users;
using PGB.Application.Interfaces;

namespace PGB.Application.Models.Users.Commands;

public class AddBannedUserHandler : IRequestHandler<AddBannedUser, bool>
{
    private readonly IService _service;

    public AddBannedUserHandler(IService service) => _service = service;
    
    public async Task<bool> Handle(AddBannedUser request, CancellationToken cancellationToken)
    {
        BannedUserDto addBannedUserDto = new BannedUserDto(UserId : request.UserId);
        var state = await _service.BanUser(addBannedUserDto);
        return state;
    }
}

using AutoMapper;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Application.Interfaces;
using PGB.Application.IRepositories;
using PGB.Domain.Entities;

namespace PGB.Application.Services;

public class BookOrderService : IBookOrderService
{
    private readonly IMapper _mapper;
    private readonly IBookOrderRepository _bookOrderRepository;
    private readonly IUserOrderRepository _userOrderRepository;
    private readonly IBannedUserRepository _bannedUserRepository;

    public BookOrderService(IMapper mapper, IBookOrderRepository bookOrderRepository, IUserOrderRepository userOrderRepository, IBannedUserRepository bannedUserRepository)
    {
        _mapper = mapper;
        _bookOrderRepository = bookOrderRepository;
        _userOrderRepository = userOrderRepository;
        _bannedUserRepository = bannedUserRepository;
    }




    public async Task<bool> RegisterBookOrder(BookOrderPostDTO bookOrderPostDTO)
    {
        var bookOrder = _mapper.Map<Order>(bookOrderPostDTO);

        if (await IsUserBanned(bookOrder.UserId))
            return false;

        var user = await _userOrderRepository.GetAsync(bookOrder.UserId);

        if (user is not null && user.OrdersInCurrentMonth == Restriction.MaxOrderByMonth)
            return false;

        if (user is null)
            return await RegisterNewUserOrder(bookOrder);

        return await RegisterExistingUserOrder(user, bookOrder);
    }

    private async Task<bool> IsUserBanned(int userId)
    {
        var bannedUser = await _bannedUserRepository.GetAsync(userId);
        if (bannedUser is not null) return true;
        else return false;
    }

    private async Task<bool> RegisterNewUserOrder(Order bookOrder)
    {
        var userOrder = new UserOrder { UserId = bookOrder.UserId, OrdersInCurrentMonth = 1 };
        await _userOrderRepository.PostOrderBlockAsync(userOrder);

        return await RegisterBookOrderDetails(bookOrder);
    }

    private async Task<bool> RegisterExistingUserOrder(UserOrder user, Order bookOrder)
    {
        int value = await _userOrderRepository.IncrementOrderBlockAsync(user.UserId);

        var bookOrderState = await RegisterBookOrderDetails(bookOrder);

        if (value == Restriction.MaxOrderByMonth)
        {
            var state = await _userOrderRepository.BlockOrder(user.UserId);
            return state;
        }

        return bookOrderState;
    }

    private async Task<bool> RegisterBookOrderDetails(Order bookOrder)
    {
        bookOrder.OrderDate = DateTime.Now;
        bookOrder.ExpectedReturnDate = DateTime.Now.AddDays(7);
        return await _bookOrderRepository.PostAsync(bookOrder);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    //I added it just for test !
    public async Task<OrderDTO> GetOrderById(int id)
    {
        var order = await _bookOrderRepository.GetOrderById(id);
        var orderDto = _mapper.Map<OrderDTO>(order);
        return orderDto;
    }

    public async Task<OrderDTO> FindLastOrder(int userId)
    {
        var order = await _bookOrderRepository.FindLastOrder(userId);
        var orderDto = _mapper.Map<OrderDTO>(order);
        return orderDto;
    }
}

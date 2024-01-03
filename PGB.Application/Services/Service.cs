﻿using AutoMapper;
using PGB.Application.DTOs.BookOrderDTO;
using PGB.Application.DTOs.Users;
using PGB.Application.Interfaces;
using PGB.Application.IRepositories;
using PGB.Domain.Entities;

namespace PGB.Application.Services;

public class Service : IService
{
    private readonly IBookOrderRepository _bookOrderRepository;
    private readonly IBannedUserInfoRepository _bannedUserInfoRepository;
    private readonly IBannedUserRepository _bannedUserRepository;
    private readonly IUserPenaltyRepository _userPenaltyRepository;
    private readonly IUserOrderRepository _userOrderRepository;
    private readonly IMapper _mapper;

    public Service(IBookOrderRepository bookOrderRepository,
        IBannedUserInfoRepository bannedUserInfoRepository,
        IBannedUserRepository bannedUserRepository,
        IUserPenaltyRepository userPenaltyRepository,
        IUserOrderRepository userOrderRepository,IMapper mapper)
    {
        _bookOrderRepository = bookOrderRepository;
        _bannedUserInfoRepository = bannedUserInfoRepository;
        _bannedUserRepository = bannedUserRepository;
        _userPenaltyRepository = userPenaltyRepository;
        _userOrderRepository = userOrderRepository;
        _mapper = mapper;
    }




    
    public async Task<bool> BanUser(BannedUserDto banned)
    {
        var bannedUser = _mapper.Map<BannedUser>(banned);
        var state = await _bannedUserRepository.BanAsync(bannedUser);
        return state;
    }
    public async Task<BannedUserDto> GetBannedUser(int id)
    {
        var bannedUser = await _bannedUserRepository.GetAsync(id);
        var bannedUserDto = _mapper.Map<BannedUserDto>(bannedUser);
        return bannedUserDto;
    }
    public async Task<IEnumerable<BannedUser>> GetAllBannedUsers()
    {
        var bannedUsers = await _bannedUserRepository.GetAllAsync();
        return bannedUsers;
    }
    public async Task<IEnumerable<Book>> OrderBooks(Order bookOrder)
        => await _bookOrderRepository.Order(bookOrder);
    public Task<bool> AddPenalty(UserPenalty userPenalty)
    {
        throw new NotImplementedException();
    }
    public Task<bool> RemovePenalty(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ReturnBook(Order bookReturn)
    {
        var bannedUser = await _bannedUserRepository.GetAsync(bookReturn.UserId);
        if (bannedUser is not null)
            return false;

        var order = await _bookOrderRepository.FindLastOrder(bookReturn.UserId);
        if (order is null)
            return false;

        var returnDate = await _bookOrderRepository.PutAsync(bookReturn);

        bool tooLate = (order.ExpectedReturnDate < returnDate) ? true : false;
        if (tooLate)
        {
            var penalty = await _userPenaltyRepository.CountPenaltyAsync(bookReturn.UserId);
            if (penalty == 0)
            {
                var userPenalty = new UserPenalty() { UserId = bookReturn.UserId, PenaltiesInCurrentTrimester = 1 };
                await _userPenaltyRepository.PostPenaltyAsync(userPenalty);
                var bannedUserInfo = new BannedUserInfo()
                {
                    UserId = bookReturn.UserId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                };
                await _bannedUserInfoRepository.PostAsync(bannedUserInfo);
                var newBannedUser = new BannedUser() { UserId = bookReturn.UserId };
                await _bannedUserRepository.BanAsync(newBannedUser);
                return true;
            }
            else if (penalty == 1)
            {
                await _userPenaltyRepository.IncrementPenaltyAsync(bookReturn.UserId);
                var bannedUserInfo = new BannedUserInfo()
                {
                    UserId = bookReturn.UserId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                };
                await _bannedUserInfoRepository.PostAsync(bannedUserInfo);
                var newBannedUser = new BannedUser() { UserId = bookReturn.UserId };
                await _bannedUserRepository.BanAsync(newBannedUser);
                return true;
            }
            else
            {
                await _userPenaltyRepository.RemovePenaltyAsync(bookReturn.UserId);
                var bannedUserInfo = new BannedUserInfo()
                {
                    UserId = bookReturn.UserId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1)
                };
                await _bannedUserInfoRepository.PostAsync(bannedUserInfo);
                var newBannedUser = new BannedUser() { UserId = bookReturn.UserId };
                await _bannedUserRepository.BanAsync(newBannedUser);
                return true;
            }
        }
        return true;
    }
    public async Task<bool> SaveBookOrder(Order bookOrder)
    {
        var bannedUser = await _bannedUserRepository.GetAsync(bookOrder.UserId);
        if (bannedUser is not null)
            return false;
        

        var user = await _userOrderRepository.GetAsync(bookOrder.UserId);

        if (user is not null && user.OrdersInCurrentMonth == Restriction.MaxOrderByMonth)
            return false;

        else if (user is null)
        {
            UserOrder userOrder = new() { UserId = bookOrder.UserId, OrdersInCurrentMonth = 1 };
            await _userOrderRepository.PostOrderBlockAsync(userOrder);
            bookOrder.OrderDate = DateTime.Now;
            bookOrder.ExpectedReturnDate = DateTime.Now.AddDays(7);
            var bookOrderState = await _bookOrderRepository.PostAsync(bookOrder);
            return bookOrderState;
        }
        else
        {
            int value = await _userOrderRepository.IncrementOrderBlockAsync(user.UserId);
            bookOrder.OrderDate = DateTime.Now;
            bookOrder.ExpectedReturnDate = DateTime.Now.AddDays(7);
            var bookOrderState = await _bookOrderRepository.PostAsync(bookOrder);
            if (value == Restriction.MaxOrderByMonth)
            {
                var state = await _userOrderRepository.BlockOrder(user.UserId);
                return state;
            }
            return bookOrderState;
        }
    }
    public Task<bool> SaveUserBanInfo(BannedUserInfo bannedUserInfo)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> UnbanUser(int id)
    {
        var state = await _bannedUserRepository.UnbanAsync(id);
        return state;
    }
}
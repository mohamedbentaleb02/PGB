﻿using PGB.Application.DTOs.BookOrderDTO;
using PGB.Domain.Entities;

namespace PGB.Application.Interfaces;

public interface IBookOrderService
{
    Task<bool> RegisterBookOrder(BookOrderPostDTO bookOrderPostDTO);
     Task<OrderDTO> GetOrderById(int id);
    Task<OrderDTO> FindLastOrder(int userId);
}
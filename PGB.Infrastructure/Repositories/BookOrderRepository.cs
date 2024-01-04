﻿using Microsoft.EntityFrameworkCore;
using PGB.Application.IRepositories;
using PGB.Domain.Entities;
using PGB.Infrastructure.Data;

namespace PGB.Infrastructure.Repositories;

public class BookOrderRepository : IBookOrderRepository
{
    private readonly IDBC _db;
    public BookOrderRepository(IDBC db) => _db = db;




    public async Task<bool> PostAsync(Order bookOrder)
    {
        await _db.BookOrders.AddAsync(bookOrder);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Book>> Order(Order bookOrder)
    {
        return await Task.FromResult(bookOrder.Books);
    }

    public async Task<DateTime> PutAsync(Order bookOrder)
    {
        var order = await FindLastOrder(bookOrder.UserId);
        if (order is not null)
        {
            order.ReturnDate = DateTime.Now;
            await _db.SaveChangesAsync();
            return order.ReturnDate;
        }
        return default;
    }

    public async Task<Order> FindLastOrder(int id)
    {
        var order = await _db.BookOrders.OrderByDescending(o => o.OrderDate).Where(o => o.UserId == id).FirstOrDefaultAsync();
        return order;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _db.BookOrders.FindAsync(id);
            return order;
        
    }
}

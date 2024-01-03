using PGB.Domain.Entities;

namespace PGB.Application.IRepositories;

public interface IBookOrderRepository
{
    Task<bool> PostAsync(Order bookOrder);
    Task<Order> FindLastOrder(int user_id);
    Task<DateTime> PutAsync(Order bookReturn);
    Task<IEnumerable<Book>> Order(Order bookOrder);
    Task<Order> GetOrderById(int id);
}

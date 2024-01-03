using PGB.Domain.Entities;

namespace PGB.Application.DTOs.BookOrderDTO;

public record BookOrderPutDTO(
    int UserId,
    IEnumerable<Book> Books,
    DateTime ReturnDate);

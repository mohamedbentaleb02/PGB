using PGB.Domain.Entities;

namespace PGB.Application.DTOs.BookOrderDTO;

public record OrderDTO(
    int UserId,
    DateTime OrderDate,
    DateTime ExpectedReturnDate,
    DateTime ReturnDate
    );

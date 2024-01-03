using PGB.Application.DTOs.BookDTO;

namespace PGB.Application.DTOs.BookOrderDTO;

public record BookOrderPostDTO(int UserId, IEnumerable<BookPostDTO> Books);

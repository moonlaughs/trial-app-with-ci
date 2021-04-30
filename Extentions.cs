using trial_api.DTOs;
using trial_api.Models;

namespace trial_api
{
    public static class Extensions
    {
        public static BookDTO AsDTO(this Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                BookName = book.BookName,
                Password = book.Password,
            };
        }
    }
}
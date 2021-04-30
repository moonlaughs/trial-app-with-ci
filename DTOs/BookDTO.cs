using System;

namespace trial_api.DTOs
{
    public record BookDTO
    {
        public Guid Id { get; init; }
        public string BookName { get; init; }
        public string Password { get; init; }
    }
}
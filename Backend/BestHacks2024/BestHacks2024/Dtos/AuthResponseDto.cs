﻿namespace BestHacks2024.Dtos
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}

﻿namespace LibraryBookInventory.Domain.Entities
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

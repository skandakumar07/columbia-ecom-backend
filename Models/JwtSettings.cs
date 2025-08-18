﻿namespace EcommerceTrail.Models
{
    public class JwtSettings
    {

        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int LifespanMinutes { get; set; } = 60;

    }
}

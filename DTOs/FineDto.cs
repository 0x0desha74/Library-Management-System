﻿namespace Bookly.APIs.DTOs
{
    public class FineDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public bool IsPaid { get; set; }
    }
}

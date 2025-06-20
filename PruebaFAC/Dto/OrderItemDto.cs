﻿namespace PruebaFAC.Dto;

public class OrderItemDto : ResponseBase
{
    public string? ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
}

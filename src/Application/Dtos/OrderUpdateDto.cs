﻿namespace Application.Dtos;

public class OrderUpdateDto
{
    public long Id { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Status { get; set; }
}

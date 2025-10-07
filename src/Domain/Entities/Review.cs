﻿namespace Domain.Entities;

public class Review
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = default!;
    public Product Product { get; set; } = default!;
}

﻿namespace AbySalto.Mid.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string ReviewerName { get; set; }
    public string ReviewerEmail { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
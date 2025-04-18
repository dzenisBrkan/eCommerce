﻿using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.WebApi.Models.ProductDto;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public float DiscountPercentage { get; set; }
    public float Rating { get; set; }
    public int Stock { get; set; }
    public List<string> Tags { get; set; }
    public string? Brand { get; set; }
    public string Sku { get; set; }
    public float Weight { get; set; }
    public Dimensions Dimensions { get; set; }
    public string WarrantyInformation { get; set; }
    public string ShippingInformation { get; set; }
    public string AvailabilityStatus { get; set; }
    public List<Review> Reviews { get; set; }
    public string ReturnPolicy { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public Meta Meta { get; set; }
    public string Thumbnail { get; set; }
    public List<string> Images { get; set; }
    public bool IsFavorite { get; set; }
}

public class Dimensions
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }
}

public class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string ReviewerName { get; set; }
    public string ReviewerEmail { get; set; }
}

public class Meta
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Barcode { get; set; }
    public string QrCode { get; set; }
}
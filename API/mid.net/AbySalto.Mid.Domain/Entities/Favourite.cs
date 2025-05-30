﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Mid.Domain.Entities;

public class Favorite
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User{ get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

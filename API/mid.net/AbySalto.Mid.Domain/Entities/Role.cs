﻿using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}
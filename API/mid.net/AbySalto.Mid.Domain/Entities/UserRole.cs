﻿using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities;

public class UserRole : IdentityUserRole<int>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
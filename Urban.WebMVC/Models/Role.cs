﻿using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
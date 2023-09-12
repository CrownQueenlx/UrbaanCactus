using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class AspNetUserClaim
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}

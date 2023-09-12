using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class ProductEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<ServingEntity> ServingEntities { get; set; } = new List<ServingEntity>();

    public virtual UserEntity User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class ProductTypeEntity
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<ProductEntity> ProductEntities { get; set; } = new List<ProductEntity>();
}

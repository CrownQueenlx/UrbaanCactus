using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class ProductEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Type { get; set; }

    public double? Price { get; set; }

    public string? Comments { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<BoutiqueEntity> BoutiqueEntities { get; set; } = new List<BoutiqueEntity>();

    public virtual ProductTypeEntity? TypeNavigation { get; set; }
}

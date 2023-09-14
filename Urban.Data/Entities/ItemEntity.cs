using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class ItemEntity
{
    public int? Product { get; set; }

    public int? Quantity { get; set; }

    public string? Blurb { get; set; }

    public virtual ProductEntity? ProductNavigation { get; set; }
}

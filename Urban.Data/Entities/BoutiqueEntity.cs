using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class BoutiqueEntity
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public string Location { get; set; } = null!;

    public string? Comments { get; set; }

    public int ProductEntity { get; set; }

    public int? Rating { get; set; }

    public virtual ProductEntity ProductEntityNavigation { get; set; } = null!;
}

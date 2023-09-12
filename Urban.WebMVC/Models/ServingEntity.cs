using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class ServingEntity
{
    public int Id { get; set; }

    public string Measurement { get; set; } = null!;

    public int Amount { get; set; }

    public int IngredientId { get; set; }

    public int ProductId { get; set; }

    public virtual IngredientEntity Ingredient { get; set; } = null!;

    public virtual ProductEntity Product { get; set; } = null!;
}

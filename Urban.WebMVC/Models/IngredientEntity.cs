using System;
using System.Collections.Generic;

namespace Urban.WebMVC.Models;

public partial class IngredientEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Ncarb { get; set; } = null!;

    public int? NcarbCt { get; set; }

    public int? Fat { get; set; }

    public int? Protein { get; set; }

    public string DefaultMeasurement { get; set; } = null!;

    public int DefaultAmount { get; set; }

    public virtual ICollection<ServingEntity> ServingEntities { get; set; } = new List<ServingEntity>();
}

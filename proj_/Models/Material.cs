using System;
using System.Collections.Generic;

namespace proj_.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int MaterialTypeId { get; set; }

    public decimal? MaterialPrice { get; set; }

    public decimal? MaterialStorageAmount { get; set; }

    public decimal? MaterialMinAmount { get; set; }

    public decimal? MaterialPackAmount { get; set; }

    public int UnitTypeId { get; set; }

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();

    public virtual MaterialType MaterialType { get; set; } = null!;

    public virtual UnitType UnitType { get; set; } = null!;
}

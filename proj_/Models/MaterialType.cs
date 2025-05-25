using System;
using System.Collections.Generic;

namespace proj_.Models;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string MaterialTypeName { get; set; } = null!;

    public decimal? MaterialTypeDefectRate { get; set; }

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}

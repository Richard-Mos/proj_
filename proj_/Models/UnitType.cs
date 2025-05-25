using System;
using System.Collections.Generic;

namespace proj_.Models;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string UnitTypeName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}

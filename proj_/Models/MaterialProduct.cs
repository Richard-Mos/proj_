using System;
using System.Collections.Generic;

namespace proj_.Models;

public partial class MaterialProduct
{
    public int MaterialId { get; set; }

    public int ProductId { get; set; }

    public decimal? MaterialAmount { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

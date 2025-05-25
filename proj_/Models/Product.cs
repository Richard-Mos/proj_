using System;
using System.Collections.Generic;

namespace proj_.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductTypeId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? ProductLong { get; set; }

    public int? ProductWidth { get; set; }

    public int? ProductHeight { get; set; }

    public string ProductArticle { get; set; } = null!;

    public decimal? ProductMinPrice { get; set; }

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();

    public virtual ProductType ProductType { get; set; } = null!;
}

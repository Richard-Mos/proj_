using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_.Models;

public class MaterialProxy
{
    public Material Material { get; set; }

    public decimal? MaterialAmount { get; set; } = 0.00M;

    public MaterialProxy( Material material)
    {
        this.Material = material;
    }

}

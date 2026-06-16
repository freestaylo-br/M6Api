using System;
using System.Collections.Generic;

namespace M6Api.Models;

public partial class ProductType
{
    public int IdProductType { get; set; }

    public string ProductType1 { get; set; } = null!;

    public decimal Coefficient { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

using System;
using System.Collections.Generic;

namespace M6Api.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public int IdProductType { get; set; }

    public string ProductName { get; set; } = null!;

    public string Article { get; set; } = null!;

    public decimal MinimumPrice { get; set; }

    public virtual ProductType IdProductTypeNavigation { get; set; } = null!;

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}
using System;
using System.Collections.Generic;

namespace M6Api.Models;

public partial class PartnerProduct
{
    public int IdPartnerProducts { get; set; }

    public int IdProduct { get; set; }

    public int IdPartner { get; set; }

    public string ProductQuantity { get; set; } = null!;

    public DateOnly SaleDate { get; set; }

    public virtual Partner IdPartnerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}

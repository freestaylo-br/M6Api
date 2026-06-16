using System;
using System.Collections.Generic;

namespace M6Api.Models;

public partial class Partner
{
    public int IdPartner { get; set; }

    public string PartnerType { get; set; } = null!;

    public string PartnerName { get; set; } = null!;

    public string DirectorLastname { get; set; } = null!;

    public string DirectorFirstname { get; set; } = null!;

    public string DirectorPatronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Index { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string HouseNumber { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}

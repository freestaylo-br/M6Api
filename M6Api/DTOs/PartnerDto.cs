namespace M6Api.DTOs;

public class PartnerDto
{
    public int IdPartner { get; set; }

    public string PartnerType { get; set; } = "";

    public string PartnerName { get; set; } = "";

    public string DirectorFullName { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public int Discount { get; set; }

    public string Rating { get; set; } = "";
}
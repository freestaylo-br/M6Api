using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M6Api.DTOs;
using M6Api.Models;

namespace M6Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartnersController : ControllerBase
{
    private readonly KarpovMasterContext _context;

    public PartnersController(
        KarpovMasterContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<
        IEnumerable<PartnerDto>>> GetPartners()
    {
        var partners =
            await _context.Partners
                .Include(x => x.PartnerProducts)
                .Select(x => new PartnerDto
                {
                    IdPartner =
                        x.IdPartner,

                    PartnerType =
                        x.PartnerType,

                    PartnerName =
                        x.PartnerName,

                    DirectorFullName =
                        x.DirectorLastname + " " +
                        x.DirectorFirstname + " " +
                        x.DirectorPatronymic,

                    DirectorLastname =
                        x.DirectorLastname,

                    DirectorFirstname =
                        x.DirectorFirstname,

                    DirectorPatronymic =
                        x.DirectorPatronymic,

                    PhoneNumber =
                        x.PhoneNumber,

                    Email =
                        x.Email,

                    Index =
                        x.Index,

                    Region =
                        x.Region,

                    City =
                        x.City,

                    Street =
                        x.Street,

                    HouseNumber =
                        x.HouseNumber,

                    Inn =
                        x.Inn,

                    Rating =
                        x.Rating,

                    Discount =
        CalculateDiscount(
            x.PartnerProducts
                .Sum(p =>
                    Convert.ToInt32(
                        p.ProductQuantity)))
                })
                .ToListAsync();

        return Ok(partners);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePartner(
    [FromBody] PartnerEditDto dto)
    {
        if (dto.Rating < 0)
        {
            return BadRequest(
                "Рейтинг не может быть отрицательным");
        }

        var partner = new Partner
        {
            PartnerType = dto.PartnerType,
            PartnerName = dto.PartnerName,

            DirectorLastname =
                dto.DirectorLastname,

            DirectorFirstname =
                dto.DirectorFirstname,

            DirectorPatronymic =
                dto.DirectorPatronymic,

            PhoneNumber =
                dto.PhoneNumber,

            Email =
                dto.Email,

            Index =
                dto.Index,

            Region =
                dto.Region,

            City =
                dto.City,

            Street =
                dto.Street,

            HouseNumber =
                dto.HouseNumber,

            Inn =
                dto.Inn,

            Rating =
                dto.Rating.ToString()
        };

        _context.Partners.Add(partner);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(
    int id,
    [FromBody] PartnerEditDto dto)
    {
        var partner =
            await _context.Partners
                .FirstOrDefaultAsync(
                    x => x.IdPartner == id);

        if (partner == null)
        {
            return NotFound();
        }

        if (dto.Rating < 0)
        {
            return BadRequest(
                "Рейтинг не может быть отрицательным");
        }

        partner.PartnerType =
            dto.PartnerType;

        partner.PartnerName =
            dto.PartnerName;

        partner.DirectorLastname =
            dto.DirectorLastname;

        partner.DirectorFirstname =
            dto.DirectorFirstname;

        partner.DirectorPatronymic =
            dto.DirectorPatronymic;

        partner.PhoneNumber =
            dto.PhoneNumber;

        partner.Email =
            dto.Email;

        partner.Index =
            dto.Index;

        partner.Region =
            dto.Region;

        partner.City =
            dto.City;

        partner.Street =
            dto.Street;

        partner.HouseNumber =
            dto.HouseNumber;

        partner.Inn =
            dto.Inn;

        partner.Rating =
            dto.Rating.ToString();

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartner(
    int id)
    {
        var partner =
            await _context.Partners
                .FirstOrDefaultAsync(
                    x => x.IdPartner == id);

        if (partner == null)
        {
            return NotFound();
        }

        _context.Partners.Remove(partner);

        await _context.SaveChangesAsync();

        return Ok();
    }

    private static int CalculateDiscount(
        int totalCount)
    {
        if (totalCount > 300000)
            return 15;

        if (totalCount >= 50000)
            return 10;

        if (totalCount >= 10000)
            return 5;

        return 0;
    }
}
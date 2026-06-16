using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M6Api.DTOs;
using M6Api.Models;

namespace M6Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartnersController : ControllerBase
{
    private readonly AnisMasterContext _context;

    public PartnersController(
        AnisMasterContext context)
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

                    PhoneNumber =
                        x.PhoneNumber,

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
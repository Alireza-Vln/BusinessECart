using BusinessECart.Service.Geographies;
using BusinessECart.Service.Geographies.Geography.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BusinessECart.RestApi.Controllers.Geographies;

[ApiController]
[Route("api/v1/geographies")]
public class GeographyController(IGeographyService geographyService) :
    ControllerBase
{
    [HttpPost("reverse")]
    public async Task<ActionResult<LocationInfoDto>> ReverseGeocode(
        [FromBody] CoordinatesInputDto dto)
    {
        var result = await geographyService.GetLocationInfoAsync(dto.Latitude, dto.Longitude);

        if (result == null)
            return NotFound("Could not find location info for these coordinates.");

        return Ok(result);
    }
}
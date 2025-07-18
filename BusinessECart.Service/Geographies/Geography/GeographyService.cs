using System.Text.Json;
using BusinessECart.Entities.Geographies;
using BusinessECart.Service.Geographies.Geography.Contracts;

namespace BusinessECart.Service.Geographies.Geography;

public class GeographyService : IGeographyService
{
    private readonly HttpClient _httpClient;

    public GeographyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GeoApp/1.0");
    }

    public async Task<LocationInfoDto?> GetLocationInfoAsync(double latitude, double longitude)
    {
        var url =
            $"https://nominatim.openstreetmap.org/reverse?lat={latitude}" +
            $"&lon={longitude}&format=json";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<NominatimReverseResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result?.Address == null) return null;

        var address = result.Address;

        var city = address.City ?? "Unknown";
        var province = address.State ?? "Unknown";

        var detailsParts = new List<string>();
        if (!string.IsNullOrWhiteSpace(address.Neighbourhood))
            detailsParts.Add(address.Neighbourhood);
        if (!string.IsNullOrWhiteSpace(address.Road))
            detailsParts.Add(address.Road);

        var details = string.Join(", ", detailsParts);

        return new LocationInfoDto
        {
            Province = province,
            City = city,
            Description = details
        };
    }
}
using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Service.Geographies.Geography.Contracts;

public interface IGeographyService : IScope
{
    Task<LocationInfoDto?> GetLocationInfoAsync(double latitude , double longitude );
}
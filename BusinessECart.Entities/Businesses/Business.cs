using BusinessECart.Entities.Commons.BassObj;

namespace BusinessECart.Entities.Businesses;

public class Business
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string phoneNumberOne { get; set; }
    public string? phoneNumberTwo { get; set; }
    public Location Location { get; set; }

}
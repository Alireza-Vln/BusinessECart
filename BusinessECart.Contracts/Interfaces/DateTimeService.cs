namespace BusinessECart.Contracts.Interfaces;

public interface IDateTimeService : IScope
{
    DateTime Now { get; }
    DateTime Today { get; }
}
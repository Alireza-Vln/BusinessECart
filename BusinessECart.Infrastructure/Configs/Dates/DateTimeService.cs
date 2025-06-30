
using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Infrastructure.Configs.Dates
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Today => DateTime.UtcNow.Date;

        public DateTime Now => DateTime.Now.ToUniversalTime();
    }
}

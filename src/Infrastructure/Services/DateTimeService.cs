using Carmax.Application.Common.Interfaces;

namespace Carmax.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

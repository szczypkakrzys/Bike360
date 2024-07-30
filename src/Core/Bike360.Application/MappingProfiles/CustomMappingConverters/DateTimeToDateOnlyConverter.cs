using AutoMapper;

namespace Bike360.Application.MappingProfiles.Customs;

public class DateTimeToDateOnlyConverter : ITypeConverter<DateTime, DateOnly>
{
    public DateOnly Convert(DateTime source, DateOnly destination, ResolutionContext context)
    {
        return DateOnly.FromDateTime(source);
    }
}


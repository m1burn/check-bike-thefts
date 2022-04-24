using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces.Repositories;

namespace CheckBikeThefts.Infrastructure;

/// <summary>
/// Configures mapping rules for <see cref="CheckBikeThefts.Infrastructure"/> objects 
/// </summary>
public static class AutoMapperConfigurator
{
    public static void Configure(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<ICityRepository.CityDto, City>()
            .AddTransform<string>(text => string.IsNullOrWhiteSpace(text) ? string.Empty : text.Trim());
    }
}
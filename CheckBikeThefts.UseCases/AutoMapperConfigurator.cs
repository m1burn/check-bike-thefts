using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.RiskAssessment;

namespace CheckBikeThefts.UseCases;

/// <summary>
/// Configures mapping rules for <see cref="CheckBikeThefts.UseCases"/> objects 
/// </summary>
public static class AutoMapperConfigurator
{
    public static void Configure(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<City, IGetAllCities.Out.CityDto>();
    }
}
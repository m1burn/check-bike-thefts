using AutoMapper;
using CheckBikeThefts.UseCases.RiskAssessment;

namespace CheckBikeThefts.Web;

public static class AutoMapperConfigurator
{
    public static MapperConfiguration Configure()
    {
        return new MapperConfiguration(cfg =>
        {
            Infrastructure.AutoMapperConfigurator.Configure(cfg);
            UseCases.AutoMapperConfigurator.Configure(cfg);

            Configure(cfg);
        });
    }

    private static void Configure(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<IGetAllCities.Out.CityDto, Models.Home.IndexResponseModel.City>();
        cfg.CreateMap<IGetCityBikeThefts.Out, Models.StolenBike.SearchResponseModel.City>();
    }
}
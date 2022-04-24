using System.Globalization;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using CsvHelper;

namespace CheckBikeThefts.Infrastructure.Repositories;

public class CsvCityRepository : ICityRepository
{
    private readonly IApplicationConfiguration _configuration;

    public CsvCityRepository(IApplicationConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ICollection<ICityRepository.CityDto>> GetAll()
    {
        using var reader = new StreamReader(_configuration.CityCsvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return await csv.GetRecordsAsync<ICityRepository.CityDto>().ToListAsync();
    }

    public async Task<ICityRepository.CityDto?> GetById(int cityId)
    {
        return (await GetAll()).FirstOrDefault(city => city.Id == cityId);
    }
}
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using RestSharp;

namespace CheckBikeThefts.Infrastructure.Repositories;

public class ApiStolenBikeRepository : IStolenBikeRepository
{
    private readonly IApplicationConfiguration _configuration;

    public ApiStolenBikeRepository(IApplicationConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> GetStolenBikes(string city)
    {
        using var client = new RestClient(_configuration.BikeIndexBaseUrl);
        var request = new RestRequest("search/count");
        request.Parameters.AddParameter(new QueryParameter("location", city));
        var response = await client.ExecuteAsync<SearchCountResponse>(request);
        var data = ExtractData(response);
        return data.Proximity;
    }

    private T ExtractData<T>(RestResponse<T> response)
    {
        if (response.ErrorMessage != null)
        {
            throw new HttpRequestException(response.ErrorMessage);
        }

        if (response.ErrorException != null)
        {
            throw response.ErrorException;
        }

        if (!response.IsSuccessful || response.Data == null)
        {
            throw new HttpRequestException(response.Content);
        }

        return response.Data;
    }

    private record SearchCountResponse(int Proximity);
}
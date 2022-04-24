namespace CheckBikeThefts.Web.Models.StolenBike;

public record SearchResponseModel(ICollection<SearchResponseModel.City> Cities)
{
    public record City(int CityId, int StolenBikes);
}
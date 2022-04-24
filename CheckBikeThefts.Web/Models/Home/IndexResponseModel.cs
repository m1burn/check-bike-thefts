namespace CheckBikeThefts.Web.Models.Home;

public record IndexResponseModel(ICollection<IndexResponseModel.City> CurrentlyOperate, ICollection<IndexResponseModel.City> NotCurrentlyOperate)
{
    public record City(int Id, string Name, string Country);
}
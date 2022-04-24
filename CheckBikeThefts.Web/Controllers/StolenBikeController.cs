using AutoMapper;
using CheckBikeThefts.UseCases.RiskAssessment;
using CheckBikeThefts.Web.Models.StolenBike;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CheckBikeThefts.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class StolenBikeController : ControllerBase
{
    private readonly IGetCityBikeThefts _getCityBikeThefts;
    private readonly IMapper _mapper;

    public StolenBikeController(IGetCityBikeThefts getCityBikeThefts, IMapper mapper)
    {
        _getCityBikeThefts = getCityBikeThefts;
        _mapper = mapper;
    }

    /// <summary>
    /// Find number of stolen bikes for the given cities
    /// </summary>
    /// <param name="cityIds">List of city ids to search</param>
    /// <param name="force">If true, ignore internal cache and retrieve most recent value</param>
    /// <returns>Number of stolen bikes for each requested city</returns>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery(Name = "cityIds")] ICollection<int> cityIds, bool force = false)
    {
        var results = await Task.WhenAll(cityIds.Select(
            cityId => _getCityBikeThefts.Execute(new IGetCityBikeThefts.In(cityId, force))));
        var model = new SearchResponseModel(new List<SearchResponseModel.City>());

        foreach (var result in results)
        {
            result.Validation.AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Cities.Add(_mapper.Map<SearchResponseModel.City>(result.Value));
        }

        return Ok(model);
    }
}
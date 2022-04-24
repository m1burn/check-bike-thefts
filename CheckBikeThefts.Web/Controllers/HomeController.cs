using AutoMapper;
using CheckBikeThefts.UseCases.RiskAssessment;
using CheckBikeThefts.Web.Models.Home;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CheckBikeThefts.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGetAllCities _getAllCities;
    private readonly IMapper _mapper;

    public HomeController(IGetAllCities getAllCities, IMapper mapper)
    {
        _getAllCities = getAllCities;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var cities = await _getAllCities.Execute(new IGetAllCities.In());
        cities.Validation.AddToModelState(ModelState, null);

        var currentlyOperate = ModelState.IsValid
            ? _mapper.Map<ICollection<IndexResponseModel.City>>(cities.Value.CurrentlyOperate)
            : new List<IndexResponseModel.City>();
        var notCurrentlyOperate = ModelState.IsValid
            ? _mapper.Map<ICollection<IndexResponseModel.City>>(cities.Value.NotCurrentlyOperate)
            : new List<IndexResponseModel.City>();

        return View(new IndexResponseModel(currentlyOperate, notCurrentlyOperate));
    }
}
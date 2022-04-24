using AutoMapper;
using CheckBikeThefts.Infrastructure.Repositories;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.Cache;
using CheckBikeThefts.UseCases.RiskAssessment;
using CheckBikeThefts.Web;
using FluentValidation.AspNetCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddSingleton<IMapper>(new Mapper(AutoMapperConfigurator.Configure()));
builder.Services.AddSingleton<CheckFileExists>(File.Exists);
builder.Services.AddSingleton<IApplicationConfiguration, WebApplicationConfiguration>();
builder.Services.AddSingleton<ICacheService, InMemoryCacheService>();
builder.Services.AddSingleton<IApplicationLogger, NLogApplicationLogger>();
builder.Services.AddTransient<IStolenBikeRepository, ApiStolenBikeRepository>();
builder.Services.AddTransient<ICityRepository, CsvCityRepository>();
builder.Services.AddTransient<IGetCityBikeThefts, GetCityBikeTheftsHandler>();
builder.Services.AddTransient<IGetAllCities, GetAllCitiesHandler>();

// MVC
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining(typeof(CheckBikeThefts.UseCases.IUseCase<,>));
    fv.RegisterValidatorsFromAssemblyContaining(typeof(CheckBikeThefts.Domain.City));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
NLogBuilder.ConfigureNLog("nlog.config");
builder.Logging.ClearProviders();
builder.Logging.AddNLogWeb();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // MVC
    endpoints.MapControllerRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
NLog.LogManager.Shutdown();
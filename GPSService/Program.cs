using GPSService;
using GPSService.Interfaces;
using GPSService.Services;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<IVehicleLocationSimulator, VehicleLocationSimulator>();
builder.Services.AddSingleton<IRouteDataService, RouteDataService>();
builder.Services.AddSingleton<IGPSCalculationService, GPSCalculationService>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

builder.Services.AddHttpClient();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

using Microsoft.Extensions.DependencyInjection;
using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Services;

var services = new ServiceCollection();

services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<FleetService>();
services.AddSingleton<ConsoleApp>();

using var provider = services.BuildServiceProvider();

var app = provider.GetRequiredService<ConsoleApp>();
app.Run();

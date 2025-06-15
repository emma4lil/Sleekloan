using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SleekLoan.Application.Services;
using SleekLoan.Domain.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();

await builder.Build().RunAsync();

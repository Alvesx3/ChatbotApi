using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChatbotApi.Services;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço HTTPClient e o GitHubService
builder.Services.AddHttpClient<GitHubService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

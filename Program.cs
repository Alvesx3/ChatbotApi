using ChatbotApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GitHubService>();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();

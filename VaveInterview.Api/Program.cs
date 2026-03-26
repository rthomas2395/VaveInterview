using Scalar.AspNetCore;
using VaveInterview.Api.Services;
using VaveInterview.Core.Converters;

var builder = WebApplication.CreateBuilder(args);

var blazorUrl = builder.Configuration.GetValue<string>("BlazorUrl", string.Empty);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorUi", policy =>
    {
        policy.WithOrigins(blazorUrl).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new PositionJsonConverter());
});

builder.Services.AddOpenApi();

builder.Services.AddScoped<RoverService>();

var app = builder.Build();

app.UseCors("AllowBlazorUi");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

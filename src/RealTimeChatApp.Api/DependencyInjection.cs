using Asp.Versioning;

using Microsoft.OpenApi.Models;

namespace RealTimeChatApp.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(
    this IServiceCollection services)
  {
    services.AddControllers();
    services.AddApiVersioning(options =>
    {
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = ApiVersion.Default;
      options.ReportApiVersions = true;
    });
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Swagger API",
        Description = "Real Time Chat App' Swagger documentation",
      });
    });

    return services;
  }
}
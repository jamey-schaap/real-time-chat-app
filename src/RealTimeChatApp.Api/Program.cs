using RealTimeChatApp.Api;
using RealTimeChatApp.Application;
using RealTimeChatApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddPresentation();
  builder.Services.AddApplication();
  builder.Services.AddInfrastructure();
}

var app = builder.Build();
{
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Time Chat App - API V1");
    });
  }

  app.UseCors(policy => policy
    .WithOrigins("http://localhost:8000")
    .AllowAnyMethod()
    .AllowAnyHeader());
  app.UseHttpsRedirection();
  app.MapControllers();
  app.Run();
}
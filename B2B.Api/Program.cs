using B2B.Api;
using B2B.Application;
using B2B.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddSwaggerGen()
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    //app.UseInfrastructure();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}

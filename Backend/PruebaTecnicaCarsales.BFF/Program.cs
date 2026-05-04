using PruebaTecnicaCarsales.BFF.Services;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;
using PruebaTecnicaCarsales.BFF.Middleware;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IContactoService, ContactoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});




builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            );

        var result = new
        {
            title = "Error de validación en los datos enviados",
            status = 400,
            traceId = context.HttpContext.TraceIdentifier,
            errors = errors
        };

        return new BadRequestObjectResult(result);
    };
});

var app = builder.Build();
app.UseMiddleware<ErrorMiddleware>();
app.UseCors("AllowAngular");
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
public partial class Program { }


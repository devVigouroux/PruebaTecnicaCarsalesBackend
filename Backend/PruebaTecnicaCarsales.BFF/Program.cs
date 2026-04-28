using PruebaTecnicaCarsales.BFF.Services;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;
using PruebaTecnicaCarsales.BFF.Middleware;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IContactService, ContactService>();
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


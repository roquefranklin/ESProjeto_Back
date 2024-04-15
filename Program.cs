using ESProjeto_Back.Data;
using Microsoft.EntityFrameworkCore;
using ESProjeto_Back.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
builder.Services.AddRepositories();

var connectionString = builder.Configuration.GetConnectionString("MotofretaConnection");
builder.Services.AddDbContext<MotofretaContext>(opts =>
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

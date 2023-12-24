using DotNet.TeachersApi.Features;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAddTeacherHandler, AddTeacherHandler>();
builder.Services.AddScoped<IValidator<AddTeacherCommand>, AddTeacherValidator>();
builder.Services.AddScoped<IClientEvent<AddTeacherCreatedEvent>, AddTeacherCreatedClientEvent>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped(typeof(DatabaseContext));
builder.Services.AddScoped(typeof(Broker));
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

public partial class Program { }
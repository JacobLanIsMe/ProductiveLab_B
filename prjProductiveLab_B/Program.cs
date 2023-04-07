using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Services;
using ReproductiveLabDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReproductiveLabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReproductiveLabDatabase")));
builder.Services.AddScoped<ILabMainPage, LabMainPageService>();
builder.Services.AddScoped<IFunctionService, FunctionService>();
builder.Services.AddScoped<IMediumService, MediumService>();
builder.Services.AddScoped<IIncubatorService, IncubatorService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();
builder.Services.AddScoped<IOperateSpermService, OperateSpermService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IObservationNoteService, ObservationNoteService>();
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

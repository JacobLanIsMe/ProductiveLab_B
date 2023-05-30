using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Services;
using ReproductiveLab_Common.Interfaces;
using ReproductiveLab_Common.Services;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Repository.Repositories;
using ReproductiveLab_Service.Interfaces;
using ReproductiveLab_Service.Services;
using ReproductiveLabDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReproductiveLabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReproductiveLabDatabase")));
builder.Services.AddScoped<ISharedFunction, SharedFunction>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISharedFunctionService, SharedFunctionService>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
builder.Services.AddScoped<ReproductiveLab_Service.Interfaces.IFunctionService, ReproductiveLab_Service.Services.FunctionService>();
builder.Services.AddScoped<ReproductiveLab_Service.Interfaces.IMediumService, ReproductiveLab_Service.Services.MediumService>();
builder.Services.AddScoped<IMediumRepository, MediumRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICourseOfTreatmentRepository, CourseOfTreatmentRepository>();
builder.Services.AddScoped<ReproductiveLab_Service.Interfaces.ITreatmentService, ReproductiveLab_Service.Services.TreatmentService>();
builder.Services.AddScoped<IOperateSpermService, OperateSpermService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IObservationNoteService, ObservationNoteService>();
builder.Services.AddScoped<IOvumDetailRepository, OvumDetailRepository>();
builder.Services.AddScoped<ICourseOfTreatmentRepository, CourseOfTreatmentRepository>();
builder.Services.AddScoped<ReproductiveLab_Service.Interfaces.IFreezeSummaryService, ReproductiveLab_Service.Services.FreezeSummaryService>();
builder.Services.AddScoped<ITransferInService, TransferInService>();
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

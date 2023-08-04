using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using Reproductive_SharedFunction.Services;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Repository.Repositories;
using ReproductiveLab_Service.Interfaces;
using ReproductiveLab_Service.Services;
using ReproductiveLabDB.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    if (args != null)
    {
        config.AddCommandLine(args);
    }
    
});
//var a = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ReproductiveLabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ReproductiveLabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
builder.Services.AddScoped<IFunctionService, FunctionService>();
builder.Services.AddScoped<IMediumService, MediumService>();
builder.Services.AddScoped<IMediumRepository, MediumRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICourseOfTreatmentRepository, CourseOfTreatmentRepository>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();
builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddScoped<ITreatmentFunction, TreatmentFunction>();
builder.Services.AddScoped<IOperateSpermService, OperateSpermService>();
builder.Services.AddScoped<IOperateSpermRepository, OperateSpermRepository>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IObservationNoteService, ObservationNoteService>();
builder.Services.AddScoped<IObservationNoteRepository, ObservationNoteRepository>();
builder.Services.AddScoped<IOvumDetailRepository, OvumDetailRepository>();
builder.Services.AddScoped<ICourseOfTreatmentRepository, CourseOfTreatmentRepository>();
builder.Services.AddScoped<IFreezeSummaryService, FreezeSummaryService>();
builder.Services.AddScoped<ISpermFreezeRepository, SpermFreezeRepository>();
builder.Services.AddScoped<ITransferInService, TransferInService>();
builder.Services.AddScoped<ITransferInRepository, TransferInRepository>();
builder.Services.AddScoped<IErrorFunction, ErrorFunction>();
builder.Services.AddScoped<ITreatmentFunction, TreatmentFunction>();
builder.Services.AddScoped<IOvumDetailFunction, OvumDetailFunction>();
builder.Services.AddScoped<IPhotoFunction, PhotoFunction>();
builder.Services.AddScoped<IObservationNoteFunction, ObservationNoteFunction>();
builder.Services.AddScoped<IOperateSpermFunction, OperateSpermFunction>();
builder.Services.AddScoped<IOvumFreezeRepository, OvumFreezeRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

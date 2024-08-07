using CustomerManagement.Data;
using CustomerManagement.IRepositories;
using CustomerManagement.IServices;
using CustomerManagement.Mapper;
using CustomerManagement.Repository;
using CustomerManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CustomerManagementDbContext>(option => {
    option.UseSqlServer(connectionString, b => b.MigrationsAssembly("CustomerManagement.API"));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IGenderServices, GenderServices>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IStatusServices, StatusServices>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();

builder.Services.AddScoped<IEnquiryServices, EnquiryServices>();
builder.Services.AddScoped<IEnquiryRepository, EnquiryRepository>();

builder.Services.AddScoped<IEnquiryDetailServices, EnquiryDetailServices>();
builder.Services.AddScoped<IEnquiryDetailRepository, EnquiryDetailRepository>();
// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

app.Run();

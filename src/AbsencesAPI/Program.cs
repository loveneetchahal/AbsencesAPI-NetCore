using AbsencesAPI;
using AbsencesAPI.Business;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AbsencesAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
builder.Services.AddScoped<IGenericRepository<Management>, GenericRepository<Management>>();
builder.Services.AddScoped<IGenericRepository<Stats>, GenericRepository<Stats>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IGenericRepository<Absence>, GenericRepository<Absence>>();
builder.Services.AddDbContext<ApplicationDbContext>();

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

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

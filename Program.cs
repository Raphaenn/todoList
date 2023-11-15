using Microsoft.EntityFrameworkCore;
using Refit;
using TodoList.Data;
using TodoList.Mapping;
using TodoList.Models.Repository;
using TodoList.Presentation.Refit;
using TodoList.Repository;
using TodoList.Repository.Interfaces;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskSystemDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// configuration dependence injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IViaCepIntergration, ViaCepIntegration>();

builder.Services.AddRefitClient<IViaCepIntegrationRefit>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/"));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
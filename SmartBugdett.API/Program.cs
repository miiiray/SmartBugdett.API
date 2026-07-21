using Microsoft.EntityFrameworkCore;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Concrete;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Concrete;
using SmartBudgett.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartBudgett.DataAccess.Context.SmartBudgetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IIncomeService, IncomeManager>();
builder.Services.AddScoped<IExpenseService, ExpenseManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
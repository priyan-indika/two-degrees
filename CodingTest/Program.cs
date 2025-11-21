using CodingTest.Controllers.Convertors;
using CodingTest.Entities;
using CodingTest.Services;
using CodingTest.Services.Interfaces;
using CodingTest.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodingTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddScoped<AccountConvertor>();
            builder.Services.AddValidatorsFromAssembly(typeof(AccountValidator).Assembly);
            builder.Services.AddDbContext<AccountDbContext>(opt => opt.UseInMemoryDatabase("Account"));
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IOrganisationalUnitService, OrganisationalUnitService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Seed initial data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services);
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
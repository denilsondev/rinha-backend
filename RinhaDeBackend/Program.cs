using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RinhaDeBackend.Application.Services;
using RinhaDeBackend.Domain.Repositories;
using RinhaDeBackend.Infrastructure.Data;
using RinhaDeBackend.Infrastructure.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "App Rinha de Backend",
                Version = "v1",
                Description = "App desenvolvida com intuito de estudo e pratica",
                Contact = new OpenApiContact
                {
                    Name = "Denilson Carvalho",
                    Email = "denilsoncdc10@gmail.com"
                }
            });

        }
        );

        builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


        builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
        builder.Services.AddScoped<IPessoaService, PessoaService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
                c.RoutePrefix = string.Empty; // Torna o Swagger acessível na raiz "/"
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
using Marcos_Rosario_P2_AP1.Components;
using Marcos_Rosario_P2_AP1.DAL;
using Marcos_Rosario_P2_AP1.Models;
using Marcos_Rosario_P2_AP1.Services;
using Microsoft.EntityFrameworkCore;

namespace Marcos_Rosario_P2_AP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

			var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

			builder.Services.AddDbContextFactory<Context>(o => o.UseSqlServer(ConStr));

            builder.Services.AddScoped<CiudadesService>();
            builder.Services.AddScoped<CursosService>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.EntityFrameworkCore;
using MTechTest.Components;
using MTechTest.Context;
using MTechTest.Services;

namespace MTechTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MyDatabaseConnection");

            // Add services to the container.
            builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); 
            builder.Services.AddScoped<RfcValidator>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSweetAlert2(options =>
            {
                options.Theme = SweetAlertTheme.Dark;
            });
            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

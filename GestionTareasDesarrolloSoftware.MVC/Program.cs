using GestionTareasDesarolloSoftware.API.Models;
using GestionTareasDesarrolloSoftware.APIConsumer;

namespace GestionTareasDesarrolloSoftware.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crud<Proyecto>.EndPoint = "https://localhost:7251/api/Proyectos";
            Crud<Tarea>.EndPoint = "https://localhost:7251/api/Tareas";
            Crud<Usuario>.EndPoint = "https://localhost:7251/api/Usuarios";
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Persistence.Data;
using Demo.DAL.Persistence.Repositories.Departments;
using Demo.DAL.Persistence.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((options)=> {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        
            });
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

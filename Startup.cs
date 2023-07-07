using GestorDeEmails2.Data;
using GestorDeEmails2.Services.Contracts;
using GestorDeEmails2.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GestorDeEmails2
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllersWithViews ();
            services.AddDbContext<GestorDeEmails2Context>(options => options.UseSqlServer(Configuration.GetConnectionString("UTNFrreConnection")));

            //se agregan los servicios
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/IniciarSesion";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                });

            services.AddControllersWithViews(options =>
            {

                options.Filters.Add
                (
                    new ResponseCacheAttribute
                    {
                        NoStore = true,
                        Location = ResponseCacheLocation.None
                    }    
                );

            });


            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gestor De Emails API",
                    Version = "v1",
                    Description = "Gestor de Emails",
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor De Emails V1");
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();// siempre antes de "UseAuthorization" sino no sale del View "Iniciar Sesion"

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");
                endpoints.MapControllers();
            });
        }

    }
}

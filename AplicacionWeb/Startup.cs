using AccesoDatos;
using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
using LogicaNegocio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace AplicacionWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BancaBasicaConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddResponseCompression(options => options.EnableForHttps = true);
            services.AddScoped(typeof(IClientes), typeof(Clientes));
            services.AddScoped(typeof(ICuentas), typeof(Cuentas));
            services.AddScoped(typeof(IMovimientos), typeof(Movimientos));
            services.AddScoped(typeof(IEstadosCuenta), typeof(EstadosCuenta));
            services.AddScoped(typeof(IUsuariosAplicacion), typeof(UsuariosAplicacion));
            services.AddScoped(typeof(IRepositorioClientes), typeof(RepositorioClientes));
            services.AddScoped(typeof(IRepositorioCuentas), typeof(RepositorioCuentas));
            services.AddScoped(typeof(IRepositorioMovimientos), typeof(RepositorioMovimientos));
            services.AddScoped(typeof(IRepositorioUsuariosAplicacion), typeof(RepositorioUsuariosAplicacion));
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}

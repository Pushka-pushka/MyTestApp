using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyTestApp.Domain;
using MyTestApp.Domain.Repositories.Abstract;
using MyTestApp.Domain.Repositories.EntityFreamwork;
using MyTestApp.infrastucture;

namespace MyTestApp
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        


            //подключаем конфигурацию appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //оборачиваем секцию Project в объектную форму для кайфа

            IConfiguration configuration = configBuild.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;


            //подключение контекста базы данных
            builder.Services.AddDbContext<AppDbContext>(x=>x.UseSqlServer(config.DataBase.ConnectionString)
            //подавляем предупреждение об ошибке
            .ConfigureWarnings(warnings=>warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
             
            builder.Services.AddTransient<IServiceCategoriesRepository, EFServiceCategoryRepository>();
            builder.Services.AddTransient<IServicesRepository, EFServicesRepository>();
            builder.Services.AddTransient<DataManager>();
            //настраиваем Identity систему

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false; //использование символов в пароле
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false; //использзование цифр в пароле

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // настраиваем auth cooke
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/admin/login";
                options.AccessDeniedPath = "/admin/accessdenied";
                options.SlidingExpiration = true;
            });

            
            // подключаем функционал контроллеров 
            builder.Services.AddControllersWithViews();

            //Собираем конфигурацию 
            WebApplication app = builder.Build();

            //! Порядок следования middleware очень важен. выполняется последовательно!!! будь внимательней ебалан!


            //Подключение использование статичных файлов (js, css, )
            app.UseStaticFiles();
            
            //подключаем систему маршрутизации
            app.UseRouting();

            // подключение авторизации и аутентификацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //регистрируем нужные маршуты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");



            await app.RunAsync();
        }
    }
}

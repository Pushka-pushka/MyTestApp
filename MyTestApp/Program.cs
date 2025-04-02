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



            // подключаем функционал контроллеров 
            builder.Services.AddControllersWithViews();

            //Собираем конфигурацию 
            WebApplication app = builder.Build();

            //! Порядок следования middleware очень важен. выполняется последовательно!!! будь внимательней ебалан!


            //Подключение использование статичных файлов (js, css, )
            app.UseStaticFiles();
            
            //подключаем систему маршрутизации
            app.UseRouting();

            //регистрируем нужные маршуты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");



            await app.RunAsync();
        }
    }
}

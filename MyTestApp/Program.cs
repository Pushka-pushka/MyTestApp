using MyTestApp.infrastucture;

namespace MyTestApp
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        


            //���������� ������������ appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //����������� ������ Project � ��������� ����� ��� �����

            IConfiguration configuration = configBuild.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;



            // ���������� ���������� ������������ 
            builder.Services.AddControllersWithViews();

            //�������� ������������ 
            WebApplication app = builder.Build();

            //! ������� ���������� middleware ����� �����. ����������� ���������������!!! ���� ������������ ������!


            //����������� ������������� ��������� ������ (js, css, )
            app.UseStaticFiles();
            
            //���������� ������� �������������
            app.UseRouting();

            //������������ ������ �������
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");



            await app.RunAsync();
        }
    }
}

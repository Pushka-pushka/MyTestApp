using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyTestApp.Domain;
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


            //����������� ��������� ���� ������
            builder.Services.AddDbContext<AppDbContext>(x=>x.UseSqlServer(config.DataBase.ConnectionString)
            //��������� �������������� �� ������
            .ConfigureWarnings(warnings=>warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
             
            //����������� edintity �������

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // ����������� auth cooke
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.Cookie.LoginPath = "/admin/login";
                options.AccessDeniedPath = "/admin/accessdenied";
                options.SlidingExpiration = true;
            });

            
            // ���������� ���������� ������������ 
            builder.Services.AddControllersWithViews();

            //�������� ������������ 
            WebApplication app = builder.Build();

            //! ������� ���������� middleware ����� �����. ����������� ���������������!!! ���� ������������ ������!


            //����������� ������������� ��������� ������ (js, css, )
            app.UseStaticFiles();
            
            //���������� ������� �������������
            app.UseRouting();

            // ����������� ����������� � ��������������

            //������������ ������ �������
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");



            await app.RunAsync();
        }
    }
}

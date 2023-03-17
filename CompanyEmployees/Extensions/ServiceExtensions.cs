using CompanyEmployees.CustomFormatters;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
        public static void AddLoggerConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void AddRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void AddServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
        public static void AddSqlDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<RepositoryContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("MsSqlConnectionString")));
        /*
         * This method replaces both AddDbContext and UseSqlServer methods and allows an 
         * easier configuration. But it doesn’t provide all of the features the AddDbContext 
         * method provides. So for more advanced options, it is recommended to use AddDbContext. 
         * We will use it throughout the rest of the project.
         */
        //public static void AddSqlDbContext(this IServiceCollection services, IConfiguration configuration) 
        //    => services.AddSqlServer<RepositoryContext>(configuration.GetConnectionString("MsSqlConnectionString"));

        public static IMvcBuilder AddCustomCSVOutputFormatter(this IMvcBuilder builder) 
            => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputCompanyDtoFormatter()));
    }
}

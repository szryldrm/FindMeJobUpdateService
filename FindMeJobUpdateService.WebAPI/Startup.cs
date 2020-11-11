using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using FindMeJobUpdateService.Business.Abstract;
using FindMeJobUpdateService.Business.Concrete;
using FindMeJobUpdateService.Core.DependencyResolvers;
using FindMeJobUpdateService.Core.Extensions;
using FindMeJobUpdateService.Core.Settings.MongoDbSettings;
using FindMeJobUpdateService.Core.Settings.RedisSettings;
using FindMeJobUpdateService.Core.Utilities.IoC;
using FindMeJobUpdateService.DataAccess.Abstract;
using FindMeJobUpdateService.DataAccess.Concrete;

namespace FindMeJobUpdateService.WebAPI
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
            //services.AddDbContext<FindMeJobUpdateServiceSqlContext>(
            //    options => options.UseSqlServer(Configuration.GetSection("MSSQLDbSettings").GetSection("ConnectionString").Value));
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            //services.Configure<MSSQLDbSettings>(Configuration.GetSection("MSSQLDbSettings"));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddSingleton<IRedisSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value);
            //services.AddSingleton<IMSSQLDbSettings>(serviceProvider =>
            //serviceProvider.GetRequiredService<IOptions<MSSQLDbSettings>>().Value);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("RedisSettings").GetSection("RedisHostIP").Value
                + ":" + Configuration.GetSection("RedisSettings").GetSection("RedisPort").Value;
            });

            services.AddControllers();

            services.AddScoped<IJobsService, JobsManager>();
            services.AddScoped<IJobsDal, EfJobsDal>();

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(Configuration),
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sample.Common;
using Sample.EmployeeModule.DI;
using SampleAPI.DataProvider.Base;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Policy;

namespace SampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            EmployeeModuleDIResolver.ConfigureDI();

            services.AddDbContext<IdentityDbContext>(options =>
                                                     options.UseSqlServer(Configuration.GetConnectionString(APIDBConfig.DBName)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleAPI", Version = "v1" });
                c.DocInclusionPredicate((version, desc) => (version == "v1" && (string.IsNullOrWhiteSpace(desc.GroupName))));


                c.SchemaFilter<ApplyCustomSchemaFilters>();
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles().UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleAPI v1"));
        }

        public class ApplyCustomSchemaFilters : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                var excludeProperties = new[] { "exception" };

                foreach (var prop in excludeProperties)
                    if (schema.Properties.ContainsKey(prop))
                    {
                        schema.Properties.Remove(prop);
                    }
            }
        }
    }
}

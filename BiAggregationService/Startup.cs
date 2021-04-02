using AggregationService.Models;
using AggregationService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace AggregationService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly XbimEditorCredentials _xbimEditorCredentials;
        private readonly string _localFilePath;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _localFilePath = configuration.GetValue<string>("LocalFilePath");
            var xbimEditorCredentialConfiguration = configuration.GetSection("XbimEditorCredentials").Get<XbimEditorCredentialConfiguration>();

            _xbimEditorCredentials = new XbimEditorCredentials
            {
                ApplicationDevelopersName = xbimEditorCredentialConfiguration.ApplicationDevelopersName,
                ApplicationFullName = xbimEditorCredentialConfiguration.ApplicationFullName,
                ApplicationVersion = xbimEditorCredentialConfiguration.ApplicationVersion,
                ApplicationIdentifier = xbimEditorCredentialConfiguration.ApplicationIdentifier,
                EditorsFamilyName = xbimEditorCredentialConfiguration.EditorsFamilyName,
                EditorsGivenName = xbimEditorCredentialConfiguration.EditorsGivenName,
                EditorsOrganisationName = xbimEditorCredentialConfiguration.EditorsOrganisationName
            };
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(IfcStore.Open(_localFilePath, _xbimEditorCredentials));

            services.AddSingleton<IIfcStoreRepository<IIfcElement>>(svc => new IfcStoreRepository<IIfcElement>(svc.GetRequiredService<IfcStore>(), svc.GetRequiredService<ILogger>()));
            services.AddSingleton<IIfcStoreRepository<IIfcSpace>>(svc => new IfcStoreRepository<IIfcSpace>(svc.GetRequiredService<IfcStore>(), svc.GetRequiredService<ILogger>()));

            services.AddSingleton<ISummaryService>(svc => new SummaryService(svc.GetRequiredService<IIfcStoreRepository<IIfcElement>>()));
            services.AddSingleton<IRoomService>(svc => new RoomService(svc.GetRequiredService<IIfcStoreRepository<IIfcSpace>>()));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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
        }
    }
}

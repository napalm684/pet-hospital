using System;
using BluePaw.Administration.Data;
using BluePaw.Administration.Listeners;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Steeltoe.Connector.MySql;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Tracing;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Core;

namespace BluePaw.Administration
{
    public class Startup
    {
        private const string AdminQueue = "administration_requests_queue";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthActuator(Configuration);
            services.AddMySqlConnection(Configuration);
            services.AddDbContext<BluePawDbContext>(options =>
                options.UseMySql(Configuration), ServiceLifetime.Transient);
            services.ConfigureRabbitOptions(Configuration);
            services.AddSingleton<PatientRequestsListener>();
            services.AddSingleton<Func<BluePawDbContext>>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<BluePawDbContext>();
                optionsBuilder.UseMySql(Configuration);
                return new BluePawDbContext(optionsBuilder.Options);
            });
            services.AddRabbitListeners<PatientRequestsListener>();
            services.AddControllers();
            services.AddDistributedTracing();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BluePaw.Administration", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BluePaw.Administration v1"));
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BluePawDbContext>();
                context.Database.EnsureCreated();
            }
            
            // Create queue
            var rabbitAdmin = app.ApplicationServices.GetRequiredService<RabbitAdmin>();
            var info = rabbitAdmin?.GetQueueInfo(AdminQueue);
            if (info == null)
            {
                rabbitAdmin?.DeclareQueue(new Queue(AdminQueue));
            }
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map<HealthEndpoint>();
                endpoints.MapControllers();
            });
        }
    }
}

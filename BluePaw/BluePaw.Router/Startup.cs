using BluePaw.Router.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Tracing;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace BluePaw.Router
{
    public class Startup
    {
        private const string PatientRequestsExchange = "patient_requests";
        private const string AdminQueue = "administration_requests_queue";
        private const string AdminDepartmentName = "administration";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthActuator(Configuration);
            EventBrokerSetup(services);

            services.AddControllers();
            services.AddDistributedTracing();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BluePaw.Router", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BluePaw.Router v1"));
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map<HealthEndpoint>();
                endpoints.MapControllers();
            });
        }

        private void EventBrokerSetup(IServiceCollection services)
        {
            services.ConfigureRabbitOptions(Configuration);
            services.AddRabbitExchange(PatientRequestsExchange, ExchangeType.DIRECT);
            services.AddRabbitQueue(new Queue(AdminQueue));
            services.AddRabbitBindings(
                BindingBuilder.Bind(new Queue(AdminQueue)).To(new DirectExchange(PatientRequestsExchange))
                    .With(AdminDepartmentName));
            services.AddSingleton<IMessagePublisherService, MessagePublisherService>();
        }
    }
}

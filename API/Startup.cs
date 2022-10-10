
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using Data.Entities.Context;
using Logic.Attributes;
using static Logic.Enums;
using Models;
using Logic.IUnitOfWork;
using Logic.Repository.Generic.Interfaces;

namespace API
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
            services.AddDbContext<ApplicationDbContext>(
                opts => opts
                    .UseSqlite(Configuration.GetConnectionString("dbContext"),
                        options => options
                            .MigrationsAssembly("Data")
                            ));

            var assembly = typeof(IGenericRepository<>).Assembly;
            var repositoryTypes = assembly.GetTypes().Where(x => !x.IsInterface &&
            x.GetInterface(typeof(IGenericRepository<>).Name) != null);
            foreach (var repositoryType in repositoryTypes)
            {
                var type = repositoryType.UnderlyingSystemType;
                if (type.Name != "GenericRepository`1")
                    services.AddTransient(type.GetInterface($"I{type.Name}"), type);
            }

            services.AddScoped<UnitOfWork>();

            RegisterAssemblyServices("Logic", services);
            RegisterAssemblyServices("Models", services);

            services.AddAutoMapper(typeof(Program));
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200"); // local dev app
                options.WithOrigins("http://localhost:3000"); // local dev app react
                options.AllowAnyMethod(); // TODO clean this up
                options.AllowAnyHeader(); // TODO clean this up
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net.Core.App.Template");
                    c.DisplayRequestDuration();
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        }

        private static void RegisterAssemblyServices(string assemblyName, IServiceCollection services)
        {
            Assembly.Load(assemblyName);
            //Get all types with service implementation attribute
            var serviceImplementations = AppDomain.CurrentDomain.GetAssemblies()
                .Single(a => a.GetName().Name == assemblyName)
                .GetTypes()
                .Where(t => t.IsDefined(typeof(ServiceImplementationAttribute), false));

            //Add service registration
            foreach (var serviceImplementation in serviceImplementations)
            {
                var implementationAttribute = (ServiceImplementationAttribute)serviceImplementation.GetCustomAttribute(typeof(ServiceImplementationAttribute));
                if (implementationAttribute != null)
                {
                    switch (implementationAttribute.ServiceLifetime)
                    {
                        case DependencyInjectionLifetime.Singleton:
                            services.AddSingleton(implementationAttribute.ServiceInterface, serviceImplementation);
                            break;
                        case DependencyInjectionLifetime.Scoped:
                            services.AddScoped(implementationAttribute.ServiceInterface, serviceImplementation);
                            break;
                        case DependencyInjectionLifetime.Transient:
                            services.AddTransient(implementationAttribute.ServiceInterface, serviceImplementation);
                            break;
                    }
                }
            }

        }
    }
}

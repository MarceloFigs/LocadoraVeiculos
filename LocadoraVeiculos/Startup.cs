using FluentValidation;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Repository.EFCore;
using LocadoraVeiculos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LocadoraVeiculos
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
            services.AddDbContext<LocadoraVeiculosContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocadoraVeiculos", Version = "v1" });
            });
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IValidator<Cliente>, ClienteValidator>();
            services.AddScoped<ICarroRepository, CarroRepository>();
            services.AddScoped<IValidator<Carro>, CarroValidator>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IValidator<Categoria>, CategoriaValidator>();
            services.AddScoped<IAloca��oRepository, Aloca��oRepository>();
            services.AddScoped<IValidator<Aloca��o>, Aloca��oValidator>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocadoraVeiculos v1"));
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCors(cors =>
            cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<LocadoraVeiculosContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}

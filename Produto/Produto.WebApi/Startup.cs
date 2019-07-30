using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Produto.Repository.DataContext;
using Produto.Repository.Interfaces;
using Produto.Repository.Repositorys;
using Swashbuckle.AspNetCore.Swagger;

namespace Produto.WebApi {
    public class Startup {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }
        public Startup (IConfiguration configuration,
            IHostingEnvironment currentEnvironment) {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public void ConfigureServices (IServiceCollection services) {
            services
                .AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2)
                .AddJsonOptions (json => {
                    json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                    json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            if (CurrentEnvironment.IsEnvironment ("Test")) {
                services.AddDbContext<ProdutoContext> (options => options.UseInMemoryDatabase ("ProdutoTestDB"));
                Mapper.Reset ();
            } else {

                services
                    .AddEntityFrameworkSqlServer ()
                    .AddDbContext<ProdutoContext> (opt => opt
                        .UseSqlServer (Configuration.GetConnectionString ("database"), b => b.MigrationsAssembly ("Produto.WebApi"))
                        .EnableSensitiveDataLogging (true), ServiceLifetime.Scoped);
            }

            services.AddAutoMapper ();

            this._ConfigureInjectionDependecy (services);

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info {
                    Title = "Produto API - Luis Cesar",
                        Version = "v1",
                        Description = "Desafio técnico - Tivia",
                });
            });

        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            this._ConfigureSwagger (app);

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }

        private void _ConfigureInjectionDependecy (IServiceCollection services) {
            services
                .AddScoped<ICategoriaRepository, CategoriaRepository> ()
                .AddScoped<IProdutoRepository, ProdutoRepository> ();
        }

        private void _ConfigureSwagger (IApplicationBuilder app) {
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Produto API");
            });
        }
    }
}
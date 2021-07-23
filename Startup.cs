  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=myapp.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                    c.RoutePrefix = string.Empty;
                });
                // app.Run(async context => { //Abrir o swagger ao iniciar a API
                //     context.Response.Redirect("/swagger/index.html");
                //});

            }

            app.UseHttpsRedirection(); //Redireciona requisições HTTP para HTTPS

            app.UseRouting(); //Define endpoints

            app.UseAuthorization(); // Autorização dos endpoints

            app.UseMiddleware(typeof(ErrorHandlingMiddleware)); //Tudo que vir do usuário passará pelo middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); //Mapeia os controllers para serem endpoints
        }
    }
}

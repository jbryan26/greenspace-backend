using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace TodoApi
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
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            }));
            /*services.AddDbContext<ReservationsDbContext>(opt =>
            opt.UseInMemoryDatabase("ReservationsDbContext"));*/

            services.AddDbContext<ReservationsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ReservationsDbConn")));

            /*services.AddDbContext<RoomDbContext>(opt =>
            opt.UseInMemoryDatabase("RoomDbContext"));*/

           // services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);
          

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Title", Description = "reservations api" , Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Policy");
            app.UseHttpsRedirection();

            app.UseRouting();
            //auth
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Title V1");
            });

           
        }
    }
}

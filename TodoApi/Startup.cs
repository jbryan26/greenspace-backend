using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using    Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace TodoApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            Environment.CurrentDirectory = hostingEnvironment.ContentRootPath;
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
          
            //Init();
        }

        /*void Init()
        {
            var dataDirConfig = $"{_hostingEnvironment.ContentRootPath}/../data";
            dataDir = Path.GetFullPath(dataDirConfig);
            //read my data files from dataDir ...
        }*/

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            services.AddAutoMapper(typeof(Startup));

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            //tokens
            var SecretKey = Encoding.ASCII.GetBytes
                ("YourKey-123-askasdaskdkqweqxzmczxckasd");
            services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(token =>
                {
                    token.RequireHttpsMetadata = false;
                    token.SaveToken = true;
                    token.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        //Same Secret key will be used while creating the token
                        IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                        ValidateIssuer = false,
                        //Usually, this is your application base URL
                       // ValidIssuer = "https://somesite",
                        ValidateAudience = false,
                        //Here, we are creating and using JWT within the same application.
                        //In this case, base URL is fine.
                        //If the JWT is created using a web service, then this would be the consumer URL.
                      //  ValidAudience = "https://somesite",
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
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
            /*services.AddMvc(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              //  options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });*/
            

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

           // services.AddMvc();
            

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             //  options.SerializerSettings.Converters.Add(new Int32Converter());

            });

            

            /*services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new IntToStringConverter()));*/

            services.AddSwaggerGen(c =>
            {
               // c.DescribeAllEnumsAsStrings();
                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Title", Description = "reservations api" , Version = "v1" });
               
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddAuthorization(options => { 
                options.AddPolicy("OnlyAdmin", policy =>
                    policy.RequireClaim("AccessLevel", "Admin"));
                options.AddPolicy("OnlySuperAdmin", policy =>
                    policy.RequireClaim("AccessLevel", "SuperAdmin"));
                options.AddPolicy("OnlySiteAdmin", policy =>
                    policy.RequireClaim("AccessLevel", "SiteAdmin"));
                options.AddPolicy("OnlyAllAdmins", policy =>
                    policy.RequireClaim("AccessLevel", "SiteAdmin", "SuperAdmin", "Admin") );


            });

            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = 268435456;
               
                options.MultipartHeadersCountLimit = 100;
                // options.mul
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Policy");
            app.UseHttpsRedirection();

            app.UseRouting();
            //auth
            app.UseSession();

            // app.UseAuthorization();

            //Add JWToken to all incoming HTTP Request Header
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            //Add JWToken Authentication service
            app.UseAuthentication();
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

           // var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
           var imagesDir = $"{_hostingEnvironment.ContentRootPath}";
            if (!Directory.Exists(imagesDir))
            {
                Directory.CreateDirectory(imagesDir);
            }

            app.UseStaticFiles(); // For the wwwroot folder
            

            app.UseStaticFiles(new StaticFileOptions
            {
                /*FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),*/
                FileProvider = new PhysicalFileProvider(imagesDir),
                RequestPath = "/Uploads"
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ReservationsDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}

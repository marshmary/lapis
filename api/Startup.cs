using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lapis.API.Dtos;
using Lapis.API.Helpers;
using Lapis.API.Models;
using Lapis.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lapis.API
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
            // Get config secrets key
            string username = (Environment.GetEnvironmentVariable("UserId") ?? Configuration["UserId"]) ?? "";
            string password = (Environment.GetEnvironmentVariable("Password") ?? Configuration["Password"]) ?? "";
            string imgbbKey = (Environment.GetEnvironmentVariable("ImgBBKey") ?? Configuration["ImgBBKey"]) ?? "";
            string jwtKey = (Environment.GetEnvironmentVariable("JwtKey") ?? Configuration["JwtKey"]) ?? "";

            // Mapping the values in appsettings.json
            services.Configure<DatabaseSettings>(cfg =>
            {
                cfg.Username = username;
                cfg.Password = password;
                cfg.DatabaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
                cfg.ConnectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            });

            // Singelton provide the configuration model to project
            services.AddSingleton<IDatabaseSettings>(provider => provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            // Jwt token config
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
                };
            });

            services.AddAuthorization();

            // Project helpers
            services.AddSingleton<IImgBBHelper>(new ImgBBHelper(imgbbKey));
            services.AddSingleton<IJwtGenerator>(new JwtGenerator(jwtKey));
            services.AddSingleton<IRefreshToken, RefreshToken>();

            // Project services
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();

            // Auto mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(new ResponseDto
                    {
                        Status = 400,
                        Message = "Input value is invalid",
                        Errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() != 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            )
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lapis.API", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token (Access Token) on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lapis.API v1"));
            }

            // ReFormat exception response
            app.UseExceptionHandler(e => e.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                await context.Response.WriteAsJsonAsync(new ResponseDto(500, exception.Message));
            }));

            app.UseHttpsRedirection();

            //Get front-end url from appsettings.json
            var frontEndDevUrl = Configuration["FrontEndDevUrl"];
            var frontEndUrl = Configuration["FrontEndUrl"];

            app.UseCors(opt => opt.WithOrigins(frontEndDevUrl, frontEndUrl)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // Reformat error message when unauthorize
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto(401), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                }
                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto(403), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                }
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

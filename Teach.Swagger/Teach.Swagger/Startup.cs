using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Teach.Swagger
{
    public class Startup
    {
        private string apiVersionName="V1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c=> {

                c.SwaggerDoc(apiVersionName, new OpenApiInfo() { 
                    Version=apiVersionName,
                    Title=apiVersionName+"doc",
                    Contact=new OpenApiContact() {Url=new Uri("http://www.baidu.com"),Name="anson" }
                });


                c.IncludeXmlComments($"{Path.Combine(AppContext.BaseDirectory, "Teach.Swagger.xml")}",true);

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint($"/swagger/{apiVersionName}/swagger.json",$"{apiVersionName} doc.");

                c.RoutePrefix = "";

                c.HeadContent = "<a href='#'>sdfsdfsdf</a>";

                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Teach.Swagger.index.html"); 
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

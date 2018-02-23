using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace TodoApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
			services.AddMvc();

			//register the swagger generator, defining one or more swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info {
					Title = "My API",
					Version = "v1",
					Description= "A simple ASP.Net Core Web API I made by following a tutorial",
					TermsOfService="None",
					Contact= new Contact { Name = "Max Herrington", Url = "https://github.com/mherrington8944" }
				});

				//set the comments path for the swagger json and UI
				var basePath = AppContext.BaseDirectory;
				var xmlPath = Path.Combine(basePath, "TodoApi.xml");
				c.IncludeXmlComments(xmlPath);
			});

		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseStaticFiles();

			//enable middleware to serve generated swagger as a JSON endpoint
			app.UseSwagger();

			//enable middleware to serve swagger-ui (htmls, js, css, etc.), specifying the json endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseMvc();
		}
	}
}

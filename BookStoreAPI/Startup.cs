using BookStore.Data.Interfaces;
using BookStore.Data.Models;
using BookStore.Data.Repositories;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI
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
            // Dependency Injection
            // Whenever find ibookrepository inject bookdatabase
            services.AddTransient<IBookRepository, BookDatabase>();
          //  services.AddTransient<IBookRepository, BookRepository>();

            services.AddControllers();

            var connection = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Tanvir\Documents\BookStore.mdf; Integrated Security = True; Connect Timeout = 30";

            services.AddDbContext<BookStoreContext>
                (options => options.UseSqlServer(connection));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //for dropping and recreating database
            
            using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BookStoreContext>();
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}

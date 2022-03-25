using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Products.Services;
using ShoppingStoreApiExam.V1.Controllers.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ShoppingStoreDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=ShoppingStoreExamen;Integrated Security=True"));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IRepository<Buy>, BuyRepository>();
            services.AddTransient<IBuyService, BuyService>();

            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

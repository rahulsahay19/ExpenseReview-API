using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseReview.Data.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReimbursementApp.DatabaseHelpers;
using ReimbursementApp.DbContext;
using ReimbursementApp.EFRepository;
using ReimbursementApp.Model;
using ReimbursementApp.SampleData;

namespace ExpenseReview_ASPNET
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
            /*services.AddEntityFrameworkSqlServer()
                .AddDbContext<ExpenseReviewDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:ExpenseReviewSPA:ConnectionString"],
                        b => b.MigrationsAssembly("ReimbursementApp.Data")));*/

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ExpenseReviewDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:ExpenseReviewSPA:ConnectionString"]));


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc();
            //Initiating Seed Data
            services.AddTransient<InitialData>();
            //DI Setup
            services.Configure<DocumentSettings>(Configuration.GetSection("DocumentSettings"));
            services.AddScoped<RepositoryFactories, RepositoryFactories>();
            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
            services.AddScoped<IExpenseReviewUOW, ExpenseReviewUOW>();

            //Setting up claims
            services.AddAuthorization(configure =>
            {
                //TODO:- Setup list of users who are admins and allowed all API Access
                //And also menus visibility
                // var windowsGroup = Configuration.GetSection("WindowsGroup").GetSection("allowedUsers").Value;
                var windowsGroup = Configuration.GetSection("WindowsGroup")
                    .GetSection("allowedUsers")
                    .GetChildren()
                    .Select(x => x.Value).ToArray();

                configure.AddPolicy("Admin", policy =>
                {
                    //Access to Admin,Manager,Finance
                    policy.RequireAuthenticatedUser();
                    if (windowsGroup != null)
                    {
                        policy.RequireRole(windowsGroup);
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, InitialData seedDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            
            });
            //Initiating from here
            seedDbContext.SeedData();
        }
    }
}

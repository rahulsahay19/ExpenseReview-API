﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseReview.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReimbursementApp.DatabaseHelpers;
using ReimbursementApp.DbContext;
using ReimbursementApp.EFRepository;
using ReimbursementApp.Helpers;
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
                /* .WithExposedHeaders("Access-Control-Allow-Origin")
                 .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))*/
                //.Build());
            });

       /*     services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });*/
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));
            });
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
            //Setting up Auth for Post Methods
            services.AddAuthorization(configure =>
            {
              configure.AddPolicy("PostMethods", policy =>
                {
                    //Access to Admin,Manager,Finance
                    policy.RequireAuthenticatedUser();
                  });
            });
            services.AddAuthentication(Microsoft.AspNetCore.Server.HttpSys.HttpSysDefaults.AuthenticationScheme);
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
                // app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");   // I needed to add this otherwise in Angular I Would get "Response with status: 0 for URL"
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Internal Server Error");
                    });
                });
            }

            app.UseCors("CorsPolicy");
            // app.UseCorsMiddleware();
            /*app.UseCors(builder =>
                builder.AllowAnyOrigin().
                AllowAnyMethod().
                AllowAnyHeader().
                AllowCredentials());*/

            app.UseStaticFiles();
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

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using ExpenseReview_ASPNET;

// The default listening address is http://localhost:5000 if none is specified.

namespace ExpenseReview_ASPNET
{
    /// <summary>
    /// Executing the "dotnet run" command in the application folder will run this app.
    /// </summary>
    public class Program
    {
        
        #region snippet_Main
        public static void Main(string[] args)
        {
            Console.WriteLine("Running demo with HTTP.sys.");

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                #region snippet_Options
                .UseHttpSys(options =>
                {
                    options.Authentication.AllowAnonymous = true;
                    options.Authentication.Schemes = AuthenticationSchemes.NTLM;
                    options.MaxConnections = 100;
                    options.MaxRequestBodySize = 30000000;
                    options.UrlPrefixes.Add("http://localhost:7000");
                })
        #endregion
                .UseStartup<Startup>()
                .Build();
        #endregion
    }
}

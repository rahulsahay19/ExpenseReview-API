using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Diagnostics;
using System.IO;
using ExpenseReviewCustomHost;
using ExpenseReview_ASPNET;
using System.Linq;

// The default listening address is http://localhost:7000 if none is specified.

namespace ExpenseReview_ASPNET
{
    /// <summary>
    /// Executing the "dotnet run" command in the application folder will run this app.
    /// </summary>
    public class Program
    {
        private static string pathToContentRoot;
        #region snippet_Main
        public static void Main(string[] args)
        {
            bool isService = true;
            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }

             pathToContentRoot = Directory.GetCurrentDirectory();
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            if (isService)
            {
                BuildWebHost(args).RunAsCustomService();
            }
            else
            {
                BuildWebHost(args).Run();
            }

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
                .UseContentRoot(pathToContentRoot)
                .UseStartup<Startup>()
                .Build();
        #endregion
    }
}

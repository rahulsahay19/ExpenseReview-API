using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;


namespace ExpenseReviewCustomHost
{
    public class CustomWebHostService : WebHostService
    {
        public CustomWebHostService(IWebHost host) : base(host)
        {
        }

        protected override void OnStarting(string[] args)
        {
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            base.OnStopping();
        }
    }

}

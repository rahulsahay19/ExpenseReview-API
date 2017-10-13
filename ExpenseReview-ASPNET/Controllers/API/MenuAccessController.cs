using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ReimbursementApp.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class MenuAccessController : Controller
    {
        public MenuAccessController(IConfiguration configuration) => Config = configuration;
        public IConfiguration Config { get; set; }

        [HttpGet("")]
        public bool Get()
        {
            var windowsGroup = Config.GetSection("WindowsGroup")
                .GetSection("allowedUsers")
                .GetChildren()
                .Select(x => x.Value).ToArray();

            return windowsGroup.Contains(User.Identity.Name);
        }
    }
}

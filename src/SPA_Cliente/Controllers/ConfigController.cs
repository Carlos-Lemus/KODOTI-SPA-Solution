using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static string AppVersion = "KodotiApp-" + DateTime.UtcNow.ToString("yyyyMMddHHmmss");

        public ConfigController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(
                new
                {
                    ApiUrl = _config.GetValue<string>("ApiUrl")
                }
                );
        }

        // config/version
        [HttpGet("version")]
        public ActionResult GetVersion()
        {
            return Ok(AppVersion);
        }

    }
}

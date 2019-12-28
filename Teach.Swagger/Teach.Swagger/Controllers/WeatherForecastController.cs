using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Teach.Swagger.Controllers
{
    /// <summary>
    /// 天气管理
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 天气预报
        /// </summary>
        /// <remarks>
        /// <code>name ； 名字</code>
        /// </remarks>
        /// <returns>返回五天天气数组</returns>
        /// <param name="weatherForecast">天气实例</param>
        /// <param name="name">名字</param>
        [HttpPost]
        [ProducesResponseType(typeof(WeatherForecast),200)]
        //[ApiExplorerSettings(IgnoreApi =true)]
        [Authorize]
        public object[] Get(WeatherForecast weatherForecast,string name)
        {
            var rng = new Random();
            return new object[] {weatherForecast,name };
        }
    }
}

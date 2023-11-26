using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly IConverter _converter;
        private readonly ILogger _logger;

        public ConverterController(IConverter converter, ILogger<ConverterController> logger)
        {
            this._converter = converter;
            this._logger = logger;
        }

        /// <summary>
        /// GetConvert
        /// </summary>
        /// <param name="sourceCurrency"></param>
        /// <param name="targetCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetConvert")]
        public IActionResult GetConvert(string sourceCurrency, string targetCurrency, decimal amount)
        {
            try
            {
                var res = _converter.GetConvert(sourceCurrency, targetCurrency, amount);
                var jsonRes = JsonConvert.SerializeObject(res);
                if (jsonRes != null)
                {
                    _logger.LogInformation("Converted Successfully from " + sourceCurrency + " to " + targetCurrency,
                    DateTime.UtcNow.ToLongTimeString());
                    return Ok(jsonRes);
                }
                _logger.LogError("Some error occured while" + sourceCurrency + "to " + targetCurrency,
                    DateTime.UtcNow.ToLongTimeString());
                return BadRequest("target or source currency not found");
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occured while" + sourceCurrency + "to " + targetCurrency,
                DateTime.UtcNow.ToLongTimeString());
                return BadRequest("target or source currency not found");
            }
        }
    }
}

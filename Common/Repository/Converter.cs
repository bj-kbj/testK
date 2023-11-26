using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestApi.Models;

namespace Common.Repository
{
    public class Converter : IConverter
    {
        /// <summary>
        /// GetConvert
        /// </summary>
        /// <param name="sourceCurrency"></param>
        /// <param name="targetCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Response GetConvert(string sourceCurrency, string targetCurrency, decimal amount)
        {
            try
            {
                var response = new Response();
                decimal result;
                using (StreamReader r = new StreamReader("exchangeRates.json"))
                {
                    var json = r.ReadToEnd();
                    var getJsonValues = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(json);
                    string currencyToConvert = (sourceCurrency + "_TO_" + targetCurrency).ToUpper();
                    var chk = Environment.GetEnvironmentVariable(currencyToConvert);
                    if (!string.IsNullOrEmpty(chk))
                    {
                        result = Decimal.Parse(Environment.GetEnvironmentVariable(currencyToConvert));
                    }
                    else
                    {
                        result = (getJsonValues[currencyToConvert]);
                    }
                    response.ExchangeRate = result;
                    response.ConvertedAmount = result*amount;
                }
                return response;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

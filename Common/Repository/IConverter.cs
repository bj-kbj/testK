using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Models;

namespace Common.Repository
{
    public interface IConverter
    {
        public Response GetConvert(string sourceCurrency, string targetCurrency, decimal amount);

    }
}

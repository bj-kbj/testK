using Common.Repository;
using Moq;
using System.Xml.Linq;
using TestApi.Models;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestGetConvertHappyPath()
        {
            // Arrange
            var mock = new Mock<IConverter>();
            var response = new Response();
            string sourceCurrency = "USD"; string targetCurrency = "INR"; decimal amount = 9;
            response.ExchangeRate = 74.00M;
            response.ConvertedAmount = 74.00M * amount;
            mock.Setup(x => x.GetConvert(sourceCurrency, targetCurrency, amount)).Returns(response);

            // Act
            IConverter iconv = new Converter();
            var results = iconv.GetConvert(sourceCurrency, targetCurrency, amount);
            
            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(response.ConvertedAmount, results.ConvertedAmount);
            Assert.AreEqual(response.ExchangeRate, results.ExchangeRate);
        }
        [TestMethod]
        public void TestGetConvertSadPath()
        {
            // Arrange
            var response = new Response();
            string sourceCurrency = "INR"; string targetCurrency = "CAN"; decimal amount = 1;
            response.ExchangeRate = 74.00M;
            response.ConvertedAmount = 74.00M * amount;
            var mock = new Mock<IConverter>();
            mock.CallBase = true;
            mock.Setup(x => x.GetConvert(sourceCurrency, targetCurrency, amount)).Returns(response);


            // Act
            IConverter iconv = new Converter();
            var results = iconv.GetConvert(sourceCurrency, targetCurrency, amount);

            // Assert
            Assert.IsNull(results);
        }
    }
}
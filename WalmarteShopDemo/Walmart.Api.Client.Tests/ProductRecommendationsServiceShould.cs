using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walmart.Api.Client.Configuration;
using Walmart.Api.Client.DTO;
using Xunit;

namespace Walmart.Api.Client.Tests
{
    public class ProductRecommendationsServiceShould
    {
        [Fact]
        public void ReturnRecommendationsForProduct()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            var responseDataSample = ApiTestDataHelper
                .GetApiResponseDataFromResource("Walmart.Api.Client.Tests.ProductRecommendationSampleResponse.json");

            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(responseDataSample));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();

            IWalmartApiService productService =
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);

            var itemId = 36461211;

            //Act
            var result = productService.GetProductRecommendationAsync(itemId).Result;
            //Assert
            Assert.False(result.HasError);
            Assert.Equal(11, result.Response.Count);
            mockWalmartApiRequestManager.VerifyAll();
        }

        [Fact]
        public void ReturnEmptyResponseWhenRecommendationServiceFails()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            var responseDataSample = "{\"errors\":[{\"code\":4022,\"message\":\"No recommendations found for item 36461211\"}]}";

            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(responseDataSample));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();

            IWalmartApiService productService =
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);

            var itemId = 36461211;

            //Act
            var result = productService.GetProductRecommendationAsync(itemId).Result;
            //Assert
            Assert.False(result.HasError);
            Assert.Null(result.Response);
            mockWalmartApiRequestManager.VerifyAll();
        }
    }
}

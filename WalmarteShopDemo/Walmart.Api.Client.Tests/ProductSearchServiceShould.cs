using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Walmart.Api.Client.Configuration;
using Walmart.Api.Client.DTO;
using Xunit;

namespace Walmart.Api.Client.Tests
{
    public class ProductSearchServiceShould
    {
        [Fact]
        public void ReturnItemsForQuery()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            var responseDataSample = ApiTestDataHelper
                .GetApiResponseDataFromResource("Walmart.Api.Client.Tests.SearchApiSampleResponse.json");

            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(responseDataSample));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();

            IWalmartApiService productService = 
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);
            
            SearchRequestInput request = new SearchRequestInput
            {
                query = "Ben & Jerry's ice cream",
                sort = SearchRequestSortType.bestseller
            };

            //Act
            var result = productService.SearchProductsAsync(request).Result;
            //Assert
            Assert.False(result.HasError);
            Assert.Equal(10, result.Response.items.Count);
            mockWalmartApiRequestManager.VerifyAll();
        }

        [Fact]
        public void ReturnErrorCodeWhenApiCallFails()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            
            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(""));

            var mockWebResponse = new Mock<HttpWebResponse>();
            mockWebResponse.Setup(res => res.StatusCode).Returns(HttpStatusCode.BadRequest);
            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Throws(
                new WebException("Missing search query",
                                null,
                                WebExceptionStatus.UnknownError,
                                mockWebResponse.Object));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();
            IWalmartApiService productService = 
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);

            SearchRequestInput request = new SearchRequestInput
            {
                query = "Ben & Jerry's ice cream",
                sort = SearchRequestSortType.bestseller
            };

            //Act
            var result = productService.SearchProductsAsync(request).Result;

            //Assert
            Assert.True(result.HasError);
            Assert.Equal("400", result.ErrorCode);
            mockWalmartApiRequestManager.VerifyAll();
            mockWebResponse.VerifyAll();
        }
    }
}

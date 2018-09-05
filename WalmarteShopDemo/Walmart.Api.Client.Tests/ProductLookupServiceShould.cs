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
    public class ProductLookupServiceShould
    {
        [Fact]
        public void ReturnItemDetailForSingleProduct()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            var responseDataSample = ApiTestDataHelper
                .GetApiResponseDataFromResource("Walmart.Api.Client.Tests.ProductLookupSingleItemSampleResponse.json");

            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(responseDataSample));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();

            IWalmartApiService productService =
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);

            var itemId = 10294196;
            ProductLookupRequestInput request = new ProductLookupRequestInput
            {
                ids = new List<int>{ itemId }
            };

            //Act
            var result = productService.GetProductDetailAsync(request).Result;
            //Assert
            Assert.False(result.HasError);
            Assert.Single<FullResponseItem>(result.Response.items);
            Assert.Equal(itemId, result.Response.items[0].ItemId);
            mockWalmartApiRequestManager.VerifyAll();
        }

        [Fact]
        public void ReturnItemDetailsForMultipleProducts()
        {
            //Arrange
            WalmartApiConfig urls = ApiTestDataHelper.GetApiConfig();
            IApiRequestBuilder requestbuilder = new ApiRequestBuilder();
            var mockWalmartApiRequestManager = new Mock<IHttpApiRequestManager>();
            var responseDataSample = ApiTestDataHelper
                .GetApiResponseDataFromResource("Walmart.Api.Client.Tests.ProductLookupMultiItemSampleResponse.json");

            mockWalmartApiRequestManager.Setup(mgr => mgr.GetDataAsync(It.IsAny<String>()))
                .Returns(Task.FromResult<string>(responseDataSample));
            var mockLogger = new Mock<ILogger<ApiServiceBase>>();

            IWalmartApiService productService =
                new WalmartApiService(mockWalmartApiRequestManager.Object, requestbuilder, urls, mockLogger.Object);

            var itemIds = new List<int> { 10294196, 893337, 817612485, 39661524, 654023063, 10294197,
                10403812, 156205102, 20691516, 10324477 };
            ProductLookupRequestInput request = new ProductLookupRequestInput
            {
                ids = itemIds
            };

            //Act
            var result = productService.GetProductDetailAsync(request).Result;
            
            //Assert
            Assert.False(result.HasError);
            Assert.Equal(10, result.Response.items.Count);
            itemIds.ForEach( item =>
            {
                Assert.Single(result.Response.items, p => p.ItemId == item);
            });

            mockWalmartApiRequestManager.VerifyAll();
        }
    }
}

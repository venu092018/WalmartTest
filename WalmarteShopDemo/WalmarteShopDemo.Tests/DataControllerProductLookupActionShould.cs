using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Walmart.Api.Client;
using Walmart.Api.Client.DTO;
using WalmarteShopDemo.Controllers;
using WalmarteShopDemo.ViewModels;
using Xunit;

namespace WalmarteShopDemo.Tests
{
    public class DataControllerProductLookupActionShould
    {
        [Fact]
        public void ReturnProductDetails()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WalmartProductDataController>>();
            var mockApiService = new Mock<IWalmartApiService>();

            var itemId = 123;
            var productName = "tylenol a";

            var productLookupApiResponse = new WalmartApiResponse<ProductLookupResult>
            {
                HasError = false,
                Response = new ProductLookupResult
                {
                    items = new List<FullResponseItem>
                    {
                        new FullResponseItem
                        {
                            ItemId = itemId, BrandName = "", CategoryNode = "", CategoryPath = "", CustomerRating = 5, CustomerRatingImage = "",
                            LargeImage = "", LongDescription = "", Marketplace = true, MediumImage = "", ModelNumber = "", Msrp = 3.3M,
                            Name = productName, NumReviews = "4", ParentItemId = 1,ProductUrl = "", SalePrice = 3.1M, ShortDescription = "",
                            StandardShipRate = 1.2M, ThumbnailImage = "", Upc = ""
                        }
                    }
                }
            };

            mockApiService.Setup(s => s.GetProductDetailAsync(
                It.Is<ProductLookupRequestInput>(lookupParam => lookupParam.ids.Count == 1
                                    && lookupParam.ids.Contains(123))))
                .Returns(Task<WalmartApiResponse<ProductLookupResult>>.FromResult(productLookupApiResponse));


            var controller = new WalmartProductDataController(mockApiService.Object, mockLogger.Object);

            //Act
            var result = (ProductInfoFull)((OkObjectResult)controller.ProductLookup(itemId).Result).Value;

            //Assert
            Assert.Equal(itemId, result.ItemId);
            Assert.Equal(result.Name, result.Name);
            mockApiService.VerifyAll();
        }

        [Fact]
        public void ReturnOkObjectActionResult()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WalmartProductDataController>>();
            var mockApiService = new Mock<IWalmartApiService>();

            var itemId = 123;

            var productLookupApiResponse = new WalmartApiResponse<ProductLookupResult>
            {
                HasError = false,
                Response = new ProductLookupResult
                {
                    items = new List<FullResponseItem>
                    {
                        new FullResponseItem
                        {
                            ItemId = itemId
                        }
                    }
                }
            };

            mockApiService.Setup(s => s.GetProductDetailAsync(
                It.Is<ProductLookupRequestInput>(lookupParam => lookupParam.ids.Count == 1
                                    && lookupParam.ids.Contains(123))))
                .Returns(Task<WalmartApiResponse<ProductLookupResult>>.FromResult(productLookupApiResponse));
            var controller = new WalmartProductDataController(mockApiService.Object, mockLogger.Object);

            //Act
            var result = controller.ProductLookup(itemId).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            mockApiService.VerifyAll();
        }
    }
}

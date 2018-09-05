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
    public class DataControllerProductRecommendationsActionShould
    {
        [Fact]
        public void ReturnProductRecommendations()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WalmartProductDataController>>();
            var mockApiService = new Mock<IWalmartApiService>();

            var itemId = 2321;

            var recommendationApiResponse = new WalmartApiResponse<List<FullResponseItem>>
            {
                HasError = false,
                Response = new List<FullResponseItem> {
                        new FullResponseItem
                        {
                            ItemId = 651
                        },
                        new FullResponseItem
                        {
                            ItemId = 221
                        }
                    }
            };

            var productLookupApiResponse = new WalmartApiResponse<ProductLookupResult>
            {
                HasError = false,
                Response = new ProductLookupResult
                {
                    items = new List<FullResponseItem>
                    {
                        new FullResponseItem
                        {
                            ItemId = 651, BrandName = "", CategoryNode = "", CategoryPath = "", CustomerRating = 5, CustomerRatingImage = "",
                            LargeImage = "", LongDescription = "", Marketplace = true, MediumImage = "", ModelNumber = "", Msrp = 3.3M,
                            Name = "advil pm", NumReviews = "4", ParentItemId = 1,ProductUrl = "", SalePrice = 3.1M, ShortDescription = "",
                            StandardShipRate = 1.2M, ThumbnailImage = "", Upc = ""
                        },
                        new FullResponseItem
                        {
                            ItemId = 221, BrandName = "", CategoryNode = "", CategoryPath = "", CustomerRating = 2, CustomerRatingImage = "",
                            LargeImage = "", LongDescription = "", Marketplace = true, MediumImage = "", ModelNumber = "", Msrp = 2.3M,
                            Name = "Tylenol cold & flu syrup", NumReviews = "4", ParentItemId = 4,ProductUrl = "", SalePrice = 2.1M, ShortDescription = "",
                            StandardShipRate = 1.1M, ThumbnailImage = "", Upc = ""
                        }
                    }
                }
            };

            mockApiService.Setup(s => s.GetProductRecommendationAsync(It.Is<int>(val=> val == itemId)))
                .Returns(Task<WalmartApiResponse<List<FullResponseItem>>>.FromResult(recommendationApiResponse));

            mockApiService.Setup(s => s.GetProductDetailAsync(
                It.Is<ProductLookupRequestInput>(lookupParam => lookupParam.ids.Count == 2
                                    && lookupParam.ids.Contains(651) && lookupParam.ids.Contains(221))))
                .Returns(Task<WalmartApiResponse<ProductLookupResult>>.FromResult(productLookupApiResponse));


            var controller = new WalmartProductDataController(mockApiService.Object, mockLogger.Object);

            //Act
            var result = (List<ProductInfoFull>)((OkObjectResult)controller.ProductRecommendations(itemId).Result).Value;

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Single<ProductInfoFull>(result, item => item.ItemId == 651 && item.Name == "advil pm");
            Assert.Single<ProductInfoFull>(result, item => item.ItemId == 221 && item.Name == "Tylenol cold & flu syrup");
            mockApiService.VerifyAll();
        }
    }
}

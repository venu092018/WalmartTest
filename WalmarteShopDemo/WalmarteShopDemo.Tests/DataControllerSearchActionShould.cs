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
    public class DataControllerSearchActionShould
    {
        [Fact]
        public void ReturnProductsForQuery()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WalmartProductDataController>>();
            var mockApiService = new Mock<IWalmartApiService>();
            
            var query = "tylenol";
            var sort = SearchRequestSortType.bestseller;
            var searchApiResponse = new WalmartApiResponse<SearchResult<FullResponseItem>> {
                HasError = false,
                Response = new SearchResult<FullResponseItem>
                {
                    items = new List<FullResponseItem>
                    {
                        new FullResponseItem
                        {
                            ItemId = 123
                        },
                        new FullResponseItem
                        {
                            ItemId = 456
                        }
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
                            ItemId = 123, BrandName = "", CategoryNode = "", CategoryPath = "", CustomerRating = 5, CustomerRatingImage = "",
                            LargeImage = "", LongDescription = "", Marketplace = true, MediumImage = "", ModelNumber = "", Msrp = 3.3M,
                            Name = "tylenol a", NumReviews = "4", ParentItemId = 1,ProductUrl = "", SalePrice = 3.1M, ShortDescription = "",
                            StandardShipRate = 1.2M, ThumbnailImage = "", Upc = ""
                        },
                        new FullResponseItem
                        {
                            ItemId = 456, BrandName = "", CategoryNode = "", CategoryPath = "", CustomerRating = 2, CustomerRatingImage = "",
                            LargeImage = "", LongDescription = "", Marketplace = true, MediumImage = "", ModelNumber = "", Msrp = 2.3M,
                            Name = "tylenol b", NumReviews = "4", ParentItemId = 4,ProductUrl = "", SalePrice = 2.1M, ShortDescription = "",
                            StandardShipRate = 1.1M, ThumbnailImage = "", Upc = ""
                        }
                    }
                }
            };


            mockApiService.Setup(s => s.SearchProductsAsync(
                It.Is<SearchRequestInput>(searchParam => searchParam.query == query && searchParam.sort == sort)))
                .Returns(Task<WalmartApiResponse<SearchResult<FullResponseItem>>>.FromResult(searchApiResponse));
            mockApiService.Setup(s => s.GetProductDetailAsync(
                It.Is<ProductLookupRequestInput>(lookupParam => lookupParam.ids.Count == 2 
                                    && lookupParam.ids.Contains(123)
                                    && lookupParam.ids.Contains(456))))
                .Returns(Task<WalmartApiResponse<ProductLookupResult>>.FromResult(productLookupApiResponse));


            var controller = new WalmartProductDataController(mockApiService.Object, mockLogger.Object);

            //Act
            var result = (List<ProductInfoFull>)((OkObjectResult)controller.Search(query, sort).Result).Value;

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Single<ProductInfoFull>(result, item => item.ItemId == 123);
            Assert.Single<ProductInfoFull>(result, item => item.ItemId == 456);
            mockApiService.VerifyAll();
        }
    }
}

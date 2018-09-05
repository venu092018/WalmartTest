using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Walmart.Api.Client;
using Walmart.Api.Client.DTO;
using WalmarteShopDemo.ViewModels;
using Microsoft.Extensions.Logging;

namespace WalmarteShopDemo.Controllers
{
    [Route("api/[controller]")]
    public class WalmartProductDataController : Controller
    {
        IWalmartApiService _walmartApiService;
        readonly ILogger<WalmartProductDataController> _logger;

        public WalmartProductDataController(IWalmartApiService walmartApiService, ILogger<WalmartProductDataController> logger)
        {
            _walmartApiService = walmartApiService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(typeof(List<ProductInfoFull>), 200)]
        public async Task<IActionResult> Search(string query,
            SearchRequestSortType sort = SearchRequestSortType.relevance)
        {
            var searchRequestInput = new SearchRequestInput
            {
                query = query,
                format = SearchRequestFormat.json,
                sort = sort
            };
            var productList = new List<ProductInfoFull>();
            var searchResponse = await _walmartApiService.SearchProductsAsync(searchRequestInput);
            if ((!searchResponse.HasError)
                && (searchResponse.Response.items?.Count() > 0))
            {
                var productLookupParams = new ProductLookupRequestInput
                {
                    ids = GetTopNItemIds(searchResponse.Response.items, 10)
                };
                var productLookupResponse = await _walmartApiService.GetProductDetailAsync(productLookupParams);
                productList = ProcessProductLookupResponse(productLookupResponse);
            }
            return Ok(productList);
        }

        [HttpGet]
        [Route("ProductLookup")]
        [ProducesResponseType(typeof(ProductInfoFull), 200)]
        public async Task<IActionResult> ProductLookup(int id)
        {
            var productList = new List<ProductInfoFull>();
            if (id < 1)
                return Ok(productList);
            var productLookupParams = new ProductLookupRequestInput
            {
                ids = new List<int> { id }
            };
            var productLookupResponse = await _walmartApiService.GetProductDetailAsync(productLookupParams);
            productList = ProcessProductLookupResponse(productLookupResponse);
            var item = productList.First();
            return Ok(item);
        }

        [HttpGet]
        [Route("ProductRecommendations")]
        [ProducesResponseType(typeof(List<ProductInfoFull>), 200)]
        public async Task<IActionResult> ProductRecommendations(int id)
        {
            var productList = new List<ProductInfoFull>();
            if (id < 1)
                return Ok(productList);

            var productRecommendationResponse = await _walmartApiService.GetProductRecommendationAsync(id);
            if ((!productRecommendationResponse.HasError)
                && (productRecommendationResponse.Response?.Count() > 0))
            {
                var productLookupParams = new ProductLookupRequestInput
                {
                    ids = GetTopNItemIds(productRecommendationResponse.Response, 10)
                };
                var productLookupResponse = await _walmartApiService.GetProductDetailAsync(productLookupParams);
                productList = ProcessProductLookupResponse(productLookupResponse);
            }
            return Ok(productList);
        }

        List<int> GetTopNItemIds(List<FullResponseItem> items, int topN)
        {
            return items.Select(item => item.ItemId).Distinct().Take(topN).ToList();
        }

        List<ProductInfoFull> ProcessProductLookupResponse(WalmartApiResponse<ProductLookupResult> productLookupResponse)
        {
            var productList = new List<ProductInfoFull>();
            if (!productLookupResponse.HasError)
            {
                productLookupResponse.Response.items.ForEach(
                    product => productList.Add(
                    new ProductInfoFull
                    {
                        ItemId = product.ItemId,
                        Name = product.Name,
                        ThumbnailImage = product.ThumbnailImage,
                        MediumImage = product.MediumImage,
                        LargeImage = product.LargeImage,
                        SalePrice = product.SalePrice,
                        ShortDescription = product.ShortDescription,
                        LongDescription = product.LongDescription,
                        CustomerRatingImage = product.CustomerRatingImage,
                        NumReviews = product.NumReviews
                    }));
            }
            return productList;
        }
    }
}

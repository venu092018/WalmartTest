using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Walmart.Api.Client.Configuration;
using Walmart.Api.Client.DTO;
using Microsoft.Extensions.Logging;

namespace Walmart.Api.Client
{
    public class WalmartApiService : ApiServiceBase, IWalmartApiService
    {
        public IHttpApiRequestManager _apiRequestManager;
        public IApiRequestBuilder _apiRequestBuilder;
        public WalmartApiConfig _walmartApiConfig;

        public WalmartApiService(IHttpApiRequestManager apiRequestManager,
                                    IApiRequestBuilder searchRequestBuilder,
                                    WalmartApiConfig walmartApiConfig,
                                    ILogger<ApiServiceBase> logger) : base(logger)
        {
            _apiRequestManager = apiRequestManager;
            _apiRequestBuilder = searchRequestBuilder;
            _walmartApiConfig = walmartApiConfig;
        }

        public Task<WalmartApiResponse<ProductLookupResult>> 
            GetProductDetailAsync(ProductLookupRequestInput productLookupParameters)
        {
            var queryString = _apiRequestBuilder.GetProductLookupApiQueryString(productLookupParameters,
                _walmartApiConfig.ApiKey);
            var apiUrl = $"{_walmartApiConfig.ProductLookupUrl}?{queryString}";
            return GetDataFromApi<ProductLookupResult>(apiUrl);
        }

        public Task<WalmartApiResponse<List<FullResponseItem>>>
            GetProductRecommendationAsync(int itemId)
        {
            var queryString = _apiRequestBuilder.GetProductRecommendationApiQueryString(itemId,
                _walmartApiConfig.ApiKey);
            var apiUrl = $"{_walmartApiConfig.ProductRecommendationUrl}?{queryString}";
            return GetDataFromApi<List<FullResponseItem>>(apiUrl);
        }

        public Task<WalmartApiResponse<SearchResult<FullResponseItem>>>
            SearchProductsAsync(SearchRequestInput searchParameters)
        {
            var queryString = _apiRequestBuilder
                .GetSearchApiQueryString(searchParameters, _walmartApiConfig.ApiKey);
            var apiUrl = $"{_walmartApiConfig.ProductSearchUrl}?{queryString}";
            return GetDataFromApi<SearchResult<FullResponseItem>>(apiUrl);
        }

        async Task<WalmartApiResponse<T>> GetDataFromApi<T>(string apiUrl)
            where T : class
        {
            var apiResponse = string.Empty;
            try
            {
                var apiResponseTask = _apiRequestManager.GetDataAsync(apiUrl);
                apiResponse = await apiResponseTask;
                return DeserializeResponse<T>(apiResponse, null);
            }
            catch (WebException ex)
            {
                Logger.LogError(apiUrl);
                Logger.LogError(ex.ToString());
                Logger.LogError(apiResponse);
                return DeserializeResponse<T>(apiResponse, ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Walmart.Api.Client.DTO;

namespace Walmart.Api.Client
{
    public interface IWalmartApiService
    {
        Task<WalmartApiResponse<SearchResult<FullResponseItem>>>
            SearchProductsAsync(SearchRequestInput searchParameters);

        Task<WalmartApiResponse<ProductLookupResult>> 
            GetProductDetailAsync(ProductLookupRequestInput productLookupParameters);

        Task<WalmartApiResponse<List<FullResponseItem>>>
            GetProductRecommendationAsync(int itemId);
    }
}

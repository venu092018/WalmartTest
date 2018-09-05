using System.Collections.Generic;
using Walmart.Api.Client.DTO;

namespace Walmart.Api.Client
{
    public interface IApiRequestBuilder
    {
        string GetSearchApiQueryString(SearchRequestInput request, string apiKey);
        string GetProductLookupApiQueryString(ProductLookupRequestInput request, string apiKey);
        string GetProductRecommendationApiQueryString(int itemId, string apiKey);
    }
}

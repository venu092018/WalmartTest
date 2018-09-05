using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Api.Client.DTO;

namespace Walmart.Api.Client
{
    public class ApiRequestBuilder : IApiRequestBuilder
    {
        readonly string ResponseGroupValueBase = "base";

        public string GetProductLookupApiQueryString(ProductLookupRequestInput request, string apiKey)
        {
            string result = "";
            result = result
                .AppendValue<string>(nameof(apiKey), apiKey)
                .AppendValue<string>(nameof(request.ids), request.ids.GetCommaSeparatedItems());
            return result;
        }

        public string GetProductRecommendationApiQueryString(int itemId, string apiKey)
        {
            string result = "";
            result = result
                .AppendValue<string>(nameof(apiKey), apiKey)
                .AppendValue<int>(nameof(itemId), itemId);
            return result;
        }

        public string GetSearchApiQueryString(SearchRequestInput request, string apiKey)
        {
            string result = "";
            result = result
                .AppendValue<string>(nameof(apiKey), apiKey)
                .AppendValue<string>(nameof(request.query), request.query)
                .AppendValue<int>(nameof(request.numItems), request.numItems)
                .AppendValue<SearchRequestSortType>(nameof(request.sort), request.sort)
                .AppendValue<int>(nameof(request.categoryId), request.categoryId)
                .AppendValue<string>(nameof(request.responseGroup), ResponseGroupValueBase);
            return result;
        }
    }
}

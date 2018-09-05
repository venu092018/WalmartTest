using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Api.Client.Configuration
{
    public class WalmartApiConfig
    {
        public string ApiKey { get; set; }
        public string TrendingUrl { get; set; }
        public string ProductSearchUrl { get; set; }
        public string ProductLookupUrl { get; set; }
        public string ProductRecommendationUrl { get; set; }
    }
}

using System.IO;
using System.Reflection;
using Walmart.Api.Client.Configuration;

namespace Walmart.Api.Client.Tests
{
    public class ApiTestDataHelper
    {
        public static string GetApiResponseDataFromResource(string resourceName)
        {
            string resourceFile = string.Empty;

            using (var stream = typeof(ProductSearchServiceShould).GetTypeInfo().Assembly
                .GetManifestResourceStream(resourceName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    resourceFile = streamReader.ReadToEnd();
                }
            }
            return resourceFile;
        }

        public static WalmartApiConfig GetApiConfig()
        {
            return new WalmartApiConfig
            {
                ApiKey = "key",
                ProductSearchUrl = @"http://api.walmartlabslocalmachine.com/v1/search",
                TrendingUrl = @"http://api.walmartlabslocalmachine.com/v1/trends",
                ProductLookupUrl = @"http://api.walmartlabslocalmachine.com/v1/items",
                ProductRecommendationUrl = @"http://api.walmartlabslocalmachine.com/v1/nbp"
            };
        }
    }
}

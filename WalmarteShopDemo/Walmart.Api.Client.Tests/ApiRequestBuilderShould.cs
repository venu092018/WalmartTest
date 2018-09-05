using Xunit;
using Walmart.Api.Client.DTO;
using System.Collections.Generic;

namespace Walmart.Api.Client.Tests
{
    public class ApiRequestBuilderShould
    {
        [Fact]
        public void BuildSearchRequestQuery()
        {
            //Arrange
            IApiRequestBuilder builder = new ApiRequestBuilder();
            SearchRequestInput request = new SearchRequestInput
            {
                query = "Ben & Jerry's ice cream",
                sort = SearchRequestSortType.bestseller
            };

            //Act
            var result = builder.GetSearchApiQueryString(request, "test");

            //Assert
            var expectedValue = @"apiKey=test&query=Ben+%26+Jerry%27s+ice+cream&numItems=10&sort=bestseller&categoryId=0&responseGroup=base";
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void BuildProductLookupRequestQuery()
        {
            //Arrange
            IApiRequestBuilder builder = new ApiRequestBuilder();
            ProductLookupRequestInput request = new ProductLookupRequestInput
            {
                ids = new List<int> { 29372934 }
            };

            //Act
            var result = builder.GetProductLookupApiQueryString(request, "test");

            //Assert
            var expectedValue = @"apiKey=test&ids=29372934";
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void BuildProductLookupMultipleItemRequestQuery()
        {
            //Arrange
            IApiRequestBuilder builder = new ApiRequestBuilder();
            ProductLookupRequestInput request = new ProductLookupRequestInput
            {
                ids = new List<int> { 93874234, 238479023 }
            };

            //Act
            var result = builder.GetProductLookupApiQueryString(request, "test");

            //Assert
            var expectedValue = @"apiKey=test&ids=93874234%2C238479023";
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void BuildProductRecommendationsRequestQuery()
        {
            //Arrange
            IApiRequestBuilder builder = new ApiRequestBuilder();

            //Act
            var result = builder.GetProductRecommendationApiQueryString(3423433, "test");

            //Assert
            var expectedValue = @"apiKey=test&itemId=3423433";
            Assert.Equal(expectedValue, result);
        }
    }
}

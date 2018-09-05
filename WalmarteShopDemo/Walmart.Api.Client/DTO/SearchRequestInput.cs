using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Api.Client.DTO
{
    public class SearchRequestInput
    {
        public string query { get; set; }
        public int categoryId { get; set; }
        public int start { get; set; }
        public SearchRequestSortType sort { get; set; } = SearchRequestSortType.relevance;
        public SearchRequestSortOrder order { get; set; }
        public int numItems { get; set; } = 10;
        public SearchRequestFormat format { get; set; } = SearchRequestFormat.json;
        public string responseGroup { get; set; }
    }

    public enum SearchRequestSortType
    {
        relevance,
        price,
        title,
        bestseller,
        customerRating
    }

    public enum SearchRequestSortOrder
    {
        asc,
        desc
    }

    public enum SearchRequestFormat
    {
        xml,
        json
    }
}

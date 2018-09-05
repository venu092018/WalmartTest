using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Api.Client.DTO
{
    public class SearchResult<T>
    {
        public string query { get; set; }
        public string sort { get; set; }
        public string responseGroup { get; set; }
        public int totalResults { get; set; }
        public int start { get; set; }
        public int numItems { get; set; }
        public List<T> items { get; set; }
    }
}

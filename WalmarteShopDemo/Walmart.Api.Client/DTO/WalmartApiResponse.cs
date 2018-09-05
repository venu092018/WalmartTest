using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Api.Client.DTO
{
    public class WalmartApiResponse<T> where T : class
    {
        public T Response { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}

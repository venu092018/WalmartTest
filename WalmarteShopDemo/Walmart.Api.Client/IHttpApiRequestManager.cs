using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Walmart.Api.Client
{
    public interface IHttpApiRequestManager
    {
        Task<string> GetDataAsync(string url);
    }
}

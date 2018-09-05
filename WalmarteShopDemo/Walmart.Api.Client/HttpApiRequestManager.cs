using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Walmart.Api.Client
{
    public class HttpApiRequestManager : IHttpApiRequestManager
    {
        public Task<string> GetDataAsync(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadStringTaskAsync(url);
            }
        }
    }
}

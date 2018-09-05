using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Walmart.Api.Client.DTO;
using Microsoft.Extensions.Logging;

namespace Walmart.Api.Client
{
    public class ApiServiceBase
    {
        protected ILogger<ApiServiceBase> Logger {get; private set; }

        public ApiServiceBase(ILogger<ApiServiceBase> logger)
        {
            Logger = logger;
        }

        protected WalmartApiResponse<T> DeserializeResponse<T>(string response, WebException ex)
            where T : class
        {
            var apiResponse = new WalmartApiResponse<T>();
            if (ex != null)
            {
                var exceptionDetail = ReadWebException(ex);
                apiResponse.HasError = true;
                apiResponse.ErrorMessage = exceptionDetail.Details;
                apiResponse.ErrorCode = exceptionDetail.StatusCode;
                apiResponse.Response = JsonConvert.DeserializeObject<T>(apiResponse.ErrorMessage ?? string.Empty);
                return apiResponse;
            }

            apiResponse.HasError = false;
            apiResponse.ErrorCode = "";
            apiResponse.ErrorMessage = string.Empty;
            try
            {
                apiResponse.Response = JsonConvert.DeserializeObject<T>(response);
            }
            catch (JsonSerializationException serializationException)
            {
                var errorMessage = serializationException;
                Logger.LogError(response);
                Logger.LogError(errorMessage.ToString());
            }
            return apiResponse;
        }

        protected WebExceptionDetail ReadWebException(WebException ex)
        {
            string exceptionText = ex.Message.ToString();
            string errorResponseBody = string.Empty;
            string errorCode = string.Empty;

            if (ex.Response != null)
            {
                errorCode = ((int)(ex.Response as HttpWebResponse).StatusCode).ToString();
            }

            var responseStream = ex.Response?.GetResponseStream();

            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    errorResponseBody = reader.ReadToEnd();
                }
            }
            return new WebExceptionDetail
            {
                Details = errorResponseBody,
                StatusCode = errorCode
            };
        }
    }
}

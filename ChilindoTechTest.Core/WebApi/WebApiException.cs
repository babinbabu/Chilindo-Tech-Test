using ChilindoTechTest.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChilindoTechTest.Core.WebApi
{
    public class WebApiException : HttpResponseException
    {
        public WebApiError ErrorDetails { get; set; }

        public WebApiException(WebApiError webApiError)
            : base(webApiError.httpResponseMessage)
        {
            ErrorDetails = webApiError;
        }
    }

    public class WebApiError
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [JsonIgnore]
        public Enum ErrorCodeEnum { get; set; }
        [JsonIgnore]
        public int ErrorCode { get; set; }
        [JsonIgnore]
        public string ErrorCodeDescription { get; set; }
        [JsonIgnore]
        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public string Key { get; set; }
        [JsonIgnore]
        public HttpResponseMessage httpResponseMessage { get; set; }
        [JsonIgnore]
        public string JsonString { get; set; }

        public bool Successful { get; set; }
        public int? AccountNumber { get; set; }
        public Decimal? Balance { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }

        public WebApiError(Enum errorCode)
            : this(errorCode, errorCode.GetDescription(), errorCode.GetDescription(), errorCode.GetHttpStatusCode())
        { }

        public WebApiError(Enum errorCode, string errorMessage,int? accountNumber=null)
            : this(errorCode, errorCode.GetDescription(), errorMessage, errorCode.GetHttpStatusCode(), accountNumber)
        { }

        public WebApiError(Enum errorCode, string errorMessage, string key)
            : this(errorCode, errorCode.GetDescription(), errorMessage, errorCode.GetHttpStatusCode())
        {
            Key = key;
        }

        
        public WebApiError(int? accountNumber,string message)
        {
            AccountNumber = accountNumber!=null ? accountNumber.Value : 0;
            Successful = false;
            Balance = null;
            Currency = null;
            Message = message;
            httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonString),
                ReasonPhrase = ErrorCodeDescription
            };
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        }

            public WebApiError(Enum errorCode, string errorCodeDescription, string errorMessage, HttpStatusCode httpStatusCode,int? accountNumber=null)
            {
            
            AccountNumber = accountNumber;
            Successful = false;
            Balance = null;
            Currency = null;
            Message = errorMessage;
            JsonString = this.ToJsonString();
            

            httpResponseMessage = new HttpResponseMessage(errorCode.GetHttpStatusCode())
            {
                Content = new StringContent(JsonString),
                ReasonPhrase = ErrorCodeDescription
            };
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }

        public WebApiError(Exception exception)
        {
            log.Error(exception);
            if (exception.InnerException != null)
            {
                log.Error(exception.InnerException);
            }

            if (exception is System.Data.SqlClient.SqlException)
            {
                ErrorCodeEnum = WebApiErrorCode.ServerBusy;
            }
            else
            {
                ErrorCodeEnum = WebApiErrorCode.UnknownError;
            }
            ErrorCode = Convert.ToInt32(ErrorCodeEnum);
            ErrorCodeDescription = ErrorCodeEnum.GetDescription();
            ErrorMessage = ErrorCodeEnum.GetDescription();
            //if (exception.InnerException != null)
            //{
            //    ErrorMessage += " *Inner Exception* : " + exception.InnerException.Message;
            //}
            AccountNumber = 0;
            Successful = false;
            Balance = null;
            Currency = null;
            Message = ErrorCodeEnum.GetDescription();
            JsonString = this.ToJsonString();

            httpResponseMessage = new HttpResponseMessage(ErrorCodeEnum.GetHttpStatusCode())
            {
                Content = new StringContent(JsonString),
                ReasonPhrase = ErrorCodeDescription
            };
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
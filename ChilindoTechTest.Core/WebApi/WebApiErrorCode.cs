using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChilindoTechTest.Core.WebApi
{
    public enum WebApiErrorCode
    {
        [Description("Unexpected error happened in server. Please try again later.")]
        [HttpStatusCode(HttpStatusCode.InternalServerError)]
        UnknownError = 1,

        [Description("Server Busy. Please try again later.")]
        [HttpStatusCode(HttpStatusCode.InternalServerError)]
        ServerBusy = 2,

        [Description("Missing required data")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        BadRequest = 3,

        [Description("Invalid arguments")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvalidArguments = 4,

        [Description("Requested item not found")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        NotFound = 5,

        [Description("Unauthorized action")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        Unauthorized = 6,

        [Description("Unsupported Media Type")]
        [HttpStatusCode(HttpStatusCode.UnsupportedMediaType)]
        UnsupportedMediaType = 7,

        [Description("Invalid action")]
        [HttpStatusCode(HttpStatusCode.Forbidden)]
        InvalidAction = 8,

        [Description("Duplicated item")]
        [HttpStatusCode(HttpStatusCode.Forbidden)]
        Duplicated = 9,

        [Description("Requested item not registered")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        NotRegistered = 10,

        // User management
        [Description("Access token not found")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        AccessTokenNotFound = 1000,

        [Description("Device is not registered")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        DeviceNotRegistered = 1001,

        [Description("User is not registered")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        UserNotRegistered = 1002,

        [Description("The user is banned")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        UserIsBanned = 1003,

        [Description("Verification code required")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        VerificationCodeRequired = 1004,

        [Description("You have entered Invalid/Expired verification code")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        InvalidVerificationCode = 1005,

        [Description("Your session has expired. Please login again to manage your CallerTunes account.")]
        [HttpStatusCode(HttpStatusCode.Unauthorized)]
        ExpiredAccessToken = 1006,

        // Promotion management
        [Description("The promotion is finished claimed by users")]
        [HttpStatusCode(HttpStatusCode.Forbidden)]
        PromotionExceededLimit = 2001,

        [Description("The promotion is not started yet")]
        [HttpStatusCode(HttpStatusCode.Forbidden)]
        PromotionNotStart = 2002,

        [Description("The promotion is expired")]
        [HttpStatusCode(HttpStatusCode.Forbidden)]
        PromotionEnded = 2003,

        [Description("Sorry, Please update the location / provide the Delivery address")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        CoordinateRequired = 2004,

        [Description("Sorry, Please update your Makan2U app for place order")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        NewAppRequired = 2005,
    }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class HttpStatusCodeAttribute : System.Attribute
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusCodeAttribute(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
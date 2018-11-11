using ChilindoTechTest.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChilindoTechTest.Api
{
    public class WebApiWrapper
    {
        public delegate T GenericWebApi<T>(params object[] inputs);

        public static TResult Call<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return func(inputs);
            }
            catch (Exception ex)
            {
                if (ex is WebApiException)
                {
                    throw ex;
                }
                else
                {
                    throw new WebApiException(new WebApiError(ex));
                }
            }
        }

        public static async Task<TResult> CallAsync<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return await Task.Factory.StartNew<TResult>(() => { return func(inputs); });
            }
            catch (Exception ex)
            {
                if (ex is WebApiException)
                {
                    throw ex;
                }
                else if (ex is AggregateException)
                {
                    throw new WebApiException(new WebApiError(WebApiErrorCode.BadRequest, ex.InnerException.Message));
                }
                else
                {
                    throw new WebApiException(new WebApiError(ex));
                }
            }
        }

    }
}
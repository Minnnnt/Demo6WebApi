using FirstDemo6Common.Enums;
using FirstDemo6WebCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static FirstDemo6Common.Enums.ExceptionEnums;

namespace FirstDemo6WebCore.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public BusinessExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessFriendException ex)
            {                
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                // 对于非 BusinessFriendException 类型的异常，重新抛出让系统默认处理
                throw;
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ExceptionEnums.BusinessErrorCode errorCode;
            string errorMessage;
            string exceptionType = ex.GetType().FullName;
            string stackTrace = ex.StackTrace;

            if (ex is BusinessFriendException businessEx)
            {
                errorCode = businessEx.ErrorCode;
                errorMessage = businessEx.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 可以根据业务需求调整状态码
            }
            else
            {
                errorCode = BusinessErrorCode.Error;
                errorMessage = "An unexpected error occurred.";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";

            // 构建错误响应对象，包含更多错误信息
            var errorResponse = new
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ExceptionType = exceptionType,
                StackTrace = stackTrace
            };

            // 将错误响应对象序列化为 JSON 字符串
            var jsonResponse = JsonConvert.SerializeObject(errorResponse);

            // 将 JSON 响应写入响应流
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

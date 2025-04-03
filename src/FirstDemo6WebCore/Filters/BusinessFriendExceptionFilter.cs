using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessFriendExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IHostEnvironment _hosteEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// 
        /// </summary>
        public BusinessFriendExceptionFilter(
            IHostEnvironment hosteEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hosteEnvironment = hosteEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }
        /// <summary>
        /// 发生异常进入
        /// </summary>
        /// <param name="context"></param>
        public async void OnException(ExceptionContext context)
        {
            ContentResult result = new ContentResult
            {
                StatusCode = 500,
                ContentType = "text/json;charset=utf-8;"
            };

            if (_hosteEnvironment.IsDevelopment())
            {
                var json = new { message = context.Exception.Message };
                result.Content = JsonConvert.SerializeObject(json);
            }
            else
            {
                result.Content = "抱歉，出错了";
            }
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }

}

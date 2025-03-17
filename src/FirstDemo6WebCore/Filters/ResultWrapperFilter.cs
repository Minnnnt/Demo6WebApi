using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FirstDemo6Common.Models;
using FirstDemo6Common.Enums;
using FirstDemo6WebCore.Attributes;

namespace FirstDemo6WebCore.Filters
{
    /// <summary>
    /// 响应过滤器
    /// </summary>
    // 定义一个名为 ResultWrapperFilter 的类，它继承自 ActionFilterAttribute。
    // ActionFilterAttribute 是 ASP.NET Core 中用于创建过滤器的基类，
    // 该过滤器可以在动作执行前后执行特定的逻辑。
    public class ResultWrapperFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        // 重写 OnResultExecuting 方法，该方法会在控制器动作结果执行之前被调用。
        // 可以在这里对动作结果进行一些预处理操作。
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // 将 context.ActionDescriptor 转换为 ControllerActionDescriptor 类型。
            // ControllerActionDescriptor 包含了控制器和动作的详细信息，
            // 方便后续获取方法和控制器的自定义属性。
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // 获取当前动作方法上的 NoWrapperAttribute 属性实例。
            // GetCustomAttributes 方法用于获取指定类型的自定义属性，
            // 这里查找的是 NoWrapperAttribute 类型的属性，false 表示不搜索继承链。
            // FirstOrDefault 方法返回第一个匹配的属性实例，如果没有则返回 null。
            var actionWrapper = controllerActionDescriptor?.MethodInfo.GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();

            // 获取当前控制器类型上的 NoWrapperAttribute 属性实例。
            // 同样使用 GetCustomAttributes 方法查找属性，
            // ControllerTypeInfo 表示控制器的类型信息。
            var controllerWrapper = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();

            // 如果动作方法或控制器上存在 NoWrapperAttribute 属性，
            // 则说明不需要对返回结果进行包装，直接返回，不进行后续处理。
            if (actionWrapper != null || controllerWrapper != null)
            {
                return;
            }

            // 创建一个 ResponseResult<object> 类型的实例，用于包装响应结果。
            // 这里根据实际需求创建了一个通用的响应结果对象。
            var rspResult = new ResponseResult<object>();

            // 检查上下文的结果是否为 ObjectResult 类型。
            // ObjectResult 是一种常见的动作结果类型，用于返回对象数据。
            if (context.Result is ObjectResult)
            {
                // 将上下文的结果转换为 ObjectResult 类型。
                var objectResult = context.Result as ObjectResult;

                // 检查 ObjectResult 的值是否为 null。
                if (objectResult?.Value == null)
                {
                    // 如果值为 null，设置响应结果的状态为失败。
                    rspResult.Status = ResultStatus.Fail;
                    // 设置失败消息为“未找到资源”。
                    rspResult.Message = "未找到资源";
                    // 将上下文的结果替换为包装后的响应结果。
                    context.Result = new ObjectResult(rspResult);
                }
                else
                {
                    // 检查 ObjectResult 的声明类型是否为泛型类型，
                    // 并且该泛型类型的定义是否为 ResponseResult<>。
                    // 如果是，则说明返回结果已经是包装好的 ResponseResult<T> 类型，
                    // 不需要再次包装，直接返回。
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.IsGenericType && objectResult.DeclaredType?.GetGenericTypeDefinition() == typeof(ResponseResult<>))
                    {
                        return;
                    }

                    // 如果返回结果不是包装类型，将 ObjectResult 的值赋给响应结果的 Data 属性。
                    rspResult.Data = objectResult.Value;
                    // 将上下文的结果替换为包装后的响应结果。
                    context.Result = new ObjectResult(rspResult);
                }
                return;
            }
        }
    }
}

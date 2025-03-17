using FirstDemo6Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FirstDemo6Common.Models
{
    public class ResponseResult<T>
    {
        /// <summary>
        /// 状态结果
        /// </summary>
        public ResultStatus Status { get; set; } = ResultStatus.Success;

        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        private string? _msg;

        /// <summary>
        /// 消息描述
        /// </summary>
        public string? Message
        {
            get
            {
                return !string.IsNullOrEmpty(_msg) ? _msg : string.Empty;
            }
            set
            {
                _msg = value;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 成功状态返回结果
        /// </summary>
        /// <param name="result">返回的数据</param>
        /// <returns></returns>
        public static ResponseResult<T> SuccessResult(T data)
        {
            return new ResponseResult<T> { Status = ResultStatus.Success, Code = HttpStatusCode.OK, Data = data };
        }

        /// <summary>
        /// 失败状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">失败信息</param>
        /// <returns></returns>
        public static ResponseResult<T> FailResult(HttpStatusCode code, string? msg = null)
        {
            return new ResponseResult<T> { Status = ResultStatus.Fail, Code = code, Message = msg };
        }

        /// <summary>
        /// 异常状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        public static ResponseResult<T> ErrorResult(HttpStatusCode code, string? msg = null)
        {
            return new ResponseResult<T> { Status = ResultStatus.Error, Code = code, Message = msg };
        }

        /// <summary>
        /// 自定义状态返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResponseResult<T> Result(ResultStatus status, T data, HttpStatusCode code,string? msg = null)
        {
            return new ResponseResult<T> { Status = status, Code = code, Data = data, Message = msg };
        }
    }

}

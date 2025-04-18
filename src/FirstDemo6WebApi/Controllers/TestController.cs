using FirstDemo6Application.Dtos.InputDtos;
using FirstDemo6Application.Services.BusinessServices;
using FirstDemo6Application.Services.Redis;
using FirstDemo6Common.Enums;
using FirstDemo6Models.Bos.MediatRBos;
using FirstDemo6WebCore.MediatRs.Commands;
using FirstDemo6WebCore.MediatRs.Events;
using FirstDemo6WebCore.MediatRs.Requests;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace FirstDemo6WebApi.Controllers
{
    /// <summary>
    /// 
    /// ApiExplorerSettings，swagger版本控制特性
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(APIVersions.v1))]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ILogger<TestController> _Logger;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IRedisService _redis;

        /// <summary>
        /// 构造注入
        /// </summary>
        public TestController(ILogger<TestController> logger, IMediator mediator, INotificationService notificationService, IRedisService redis)
        {
            _Logger = logger;
            _mediator = mediator;
            _notificationService = notificationService;
            _redis = redis;
        }

        #region MediatR适用测试demo
        /// <summary>
        /// 发送mediatr事件send
        /// </summary>
        /// <returns></returns>
        [HttpGet("mediatRdemo/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var request = new DemoRequest { DemoId = id };
            var product = await _mediator.Send(request);
            return Ok(product);
        }

        /// <summary>
        /// 发送mediatr事件Publish
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("mediatRdemo")]
        public async Task<IActionResult> CreateProduct([FromBody] DemoCommand command)
        {
            var newProductId = await _mediator.Send(command);

            // 发布事件
            var @event = new DemoEvent { DemoId = newProductId, Name = command.Name };
            await _mediator.Publish(@event);

            return CreatedAtAction(nameof(GetProduct), new { id = newProductId }, new { Id = newProductId, Name = command.Name });
        }
        #endregion

        #region swagger测试及日志记录测试
        /// <summary>
        /// 测试swagger注释显示get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            _Logger.LogInformation("this is lognet information");
            _Logger.LogDebug("this is lognet debug");
            _Logger.LogWarning("this is lognet waring");
            _Logger.LogError("this is lognet error");
            return Ok();
        }

        /// <summary>
        /// 测试swagger注释显示post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post()
        {
            _Logger.LogInformation("this is lognet information");
            _Logger.LogDebug("this is lognet debug");
            _Logger.LogWarning("this is lognet waring");
            _Logger.LogError("this is lognet error");
            return Ok();
        }
        #endregion

        #region RabbitMq测试
        /// <summary>
        /// 创建通知RabbitMq测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("rabbitMq")]
        public async Task<IActionResult> CreateNoticeTest()
        {
            _notificationService.AcademicAffairsNotice(new AcademicAffairsNoticeDto()
            {
                NoticeMessage = "这是一个测试通知消息",
                NoticeTime = DateTime.Now.AddDays(+1),
                ExpireTime = DateTime.Now.AddDays(+2),
                NoticeType = 1,
                NoticeTopic = "测试教务通知主题",
                NoticeLevel = 0,
                IsDelayed = false,
                DelayedDuration = 0,
                NoticeScope = 0,
            });
            return Ok();
        }

        #endregion

        #region Redis测试
        [HttpGet("redis/query")]
        public IActionResult QueryRedis()
        {
            _redis.SetRedisString();
            _redis.SetRedisHash();
            _redis.SetRedisListLeft();
            _redis.SetRedisListString();
            _redis.SetRedisSorted();
            var stringValue= _redis.GetRedisString();
            var fieldValue = _redis.GetRedisHash();
            var fieldValues = _redis.GetRedisHashs();
            var listValues = _redis.GetRedisListLeft();
            var listStringValues = _redis.GetRedisListString();
            var sortedValue = _redis.GetRedisSorted();
            
            List<string> result = new List<string>
            {
                $"查询到的字符串值为: {stringValue}",
                $"哈希字段 field1 的值为: {fieldValue}",
                $"哈希字段: {string.Join(",", fieldValues.Select(x => x.Name))}, 值: {string.Join(",", fieldValues.Select(x => x.Value))}",
                $"列表元素: {string.Join(",", listValues)}",
                $"集合元素: {string.Join(",", listStringValues)}",
                $"有序集合元素: {string.Join(",", sortedValue)}"
            };
            return Ok(result);
        }
        #endregion
    }
}

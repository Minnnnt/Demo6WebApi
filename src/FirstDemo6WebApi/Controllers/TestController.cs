using FirstDemo6Common.Enums;
using FirstDemo6Models.Bos.MediatRBos;
using FirstDemo6WebCore.MediatRs.Commands;
using FirstDemo6WebCore.MediatRs.Events;
using FirstDemo6WebCore.MediatRs.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// 构造注入
        /// </summary>
        public TestController(ILogger<TestController> logger, IMediator mediator)
        {
            _Logger = logger;
            _mediator = mediator;
        }

        #region MediatR适用测试demo
        [HttpGet("mediatRdemo/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var request = new DemoRequest { DemoId = id };
            var product = await _mediator.Send(request);
            return Ok(product);
        }

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

        #region swageer测试及日志记录测试
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
    }
}

using FirstDemo6Common.Enums;
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

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="logger"></param>
        public TestController(ILogger<TestController> logger)
        {
            _Logger = logger;
        }

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
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Extensions.LoggerWraper
{
    // 非静态类用于日志记录
    public class ExtendLoggerWrapper
    {
        private readonly ILogger<ExtendLoggerWrapper> _logger;

        public ExtendLoggerWrapper(ILogger<ExtendLoggerWrapper> logger)
        {
            _logger = logger;
        }

        public void LogError(string message)
        {
            _logger?.LogError(message);
        }
    }
}

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Services.Redis
{
    public interface IRedisService : IApplicationService
    {
        RedisValue GetRedisString();
        RedisValue GetRedisHash();
        HashEntry[] GetRedisHashs();
        RedisValue[] GetRedisListLeft();
        RedisValue[] GetRedisListString();
        RedisValue[] GetRedisSorted();
        void SetRedisString();
        void SetRedisHash();
        void SetRedisListLeft();
        void SetRedisListString();
        void SetRedisSorted();
    }
}

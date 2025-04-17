using FirstDemo6Application.Services.Redis;
using FirstDemo6WebCore.Extensions.RedisExtend;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Services.Impls.Redis
{
    public class RedisService : IRedisService
    {

        public IDatabase db = RedisExtension._redisConnection.GetDatabase();
        // 字符串查询
        public RedisValue GetRedisString()
        {
            var stringValue = db.StringGet("myStringKey");
            return stringValue;
        }

        // 哈希查询
        public RedisValue GetRedisHash()
        {
            var fieldValue = db.HashGet("myHashKey", "field1");
            return fieldValue;
        }

        // 哈希查询
        public HashEntry[] GetRedisHashs()
        {
            var hashEntries = db.HashGetAll("myHashKey");
            return hashEntries;
        }

        // 列表查询
        public RedisValue[] GetRedisListLeft()
        {
            var listValues = db.ListRange("myListKey", 0, -1);
            return listValues;
        }

        // 集合查询
        public RedisValue[] GetRedisListString()
        {
            var members = db.SetMembers("mySetKey");
            return members;
        }

        // 有序集合查询
        public RedisValue[] GetRedisSorted()
        {
            var sortedMembers = db.SortedSetRangeByRank("mySortedSetKey");
            return sortedMembers;
        }

        // 字符串添加
        public void SetRedisString()
        {
            db.StringSet("myStringKey", "Hello, Redis String!");
            var stringValue = db.StringGet("myStringKey");
            if (stringValue.HasValue)
            {
                Console.WriteLine($"查询到的字符串值为: {stringValue}");
            }
            else
            {
                Console.WriteLine("未找到该字符串键对应的值");
            }
        }

        // 哈希添加
        public void SetRedisHash()
        {
            db.HashSet("myHashKey", new HashEntry[]
            {
                new HashEntry("field1", "Value1"),
                new HashEntry("field2", "Value2")
            });
            var fieldValue = db.HashGet("myHashKey", "field1");
            if (fieldValue.HasValue)
            {
                Console.WriteLine($"哈希字段 field1 的值为: {fieldValue}");
            }
            else
            {
                Console.WriteLine("未找到该哈希字段");
            }
            var hashEntries = db.HashGetAll("myHashKey");
            foreach (var entry in hashEntries)
            {
                Console.WriteLine($"哈希字段: {entry.Name}, 值: {entry.Value}");
            }
        }

        // 列表添加
        public void SetRedisListLeft()
        {
            db.ListLeftPush("myListKey", "Element1");
            db.ListLeftPush("myListKey", "Element2");
            var listLength = db.ListLength("myListKey");
            Console.WriteLine($"列表长度为: {listLength}");
            var listValues = db.ListRange("myListKey", 0, -1);
            foreach (var value in listValues)
            {
                Console.WriteLine($"列表元素: {value}");
            }
        }

        // 集合添加
        public void SetRedisListString()
        {
            db.SetAdd("mySetKey", "Member1");
            db.SetAdd("mySetKey", "Member2");
            var isMember = db.SetContains("mySetKey", "Member1");
            Console.WriteLine($"Member1 是否在集合中: {isMember}");
            var setMembers = db.SetMembers("mySetKey");
            foreach (var member in setMembers)
            {
                Console.WriteLine($"集合元素: {member}");
            }
        }

        // 有序集合添加
        public void  SetRedisSorted()
        {
            db.SortedSetAdd("mySortedSetKey", "Member1", 10);
            db.SortedSetAdd("mySortedSetKey", "Member2", 20);
            var sortedSetMembers = db.SortedSetRangeByRank("mySortedSetKey");
            foreach (var member in sortedSetMembers)
            {
                Console.WriteLine($"有序集合元素: {member}");
            }
        }
    }
}

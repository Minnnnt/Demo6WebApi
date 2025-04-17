using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Models.Bos.RedisBos
{
    public class RedisSettings
    {
        public string Name { set; get; }
        public string Ip { set; get; }
        public int Port { set; get; }
        public string Password { set; get; }
        public int Timeout { set; get; }
        public int Db { set; get; }
    }
}

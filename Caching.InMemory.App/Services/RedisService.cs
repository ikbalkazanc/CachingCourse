using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.InMemory.App.Services
{
    public class RedisService
    {
        private readonly string Host;
        private readonly string Port;
        private ConnectionMultiplexer _redis;
        public IDatabase db { get; set; }
        public RedisService(IConfiguration configuration)
        {
            Host = configuration["Redis:Host"];
            Port = configuration["Redis:Port"];
        }
        public void Connect()
        {
            var conStr = Host + ":" + Port;
            _redis = ConnectionMultiplexer.Connect(conStr);
        }
        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}

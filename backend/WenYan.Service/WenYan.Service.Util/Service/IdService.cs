﻿using Microsoft.Extensions.Configuration;

namespace WenYan.Service.Util
{
    /// <summary>
    /// 雪花算法 单例注入
    /// </summary>
    public class IdService : IIdService, ISingletonDependency
    {
        private IConfiguration Configuration { get; set; }
        private SnowflakeIdHelper IdHelper { get; set; }
        public IdService(IConfiguration configuration)
        {
            this.Configuration = configuration;
            var option = this.Configuration.GetSection("Snowflake").Get<SnowflakeConfig>();
            this.IdHelper = new SnowflakeIdHelper(option.WorkerId, option.DataCenterId);
        }
        public string GetId()
        {
            return this.IdHelper.NextId().ToString();
        }
    }
}

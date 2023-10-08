namespace WenYan.Service.Util
{
    /// <summary>
    /// 雪花算法帮助类
    /// </summary>
    public class SnowflakeIdHelper
    {
        // 开始时间戳((new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc)-Jan1st1970).TotalMilliseconds)
        private const long Twepoch = 1577836800000L;
        // 机器id所占的位数
        private const int WorkerIdBits = 5;
        // 数据标识id所占的位数
        private const int DatacenterIdBits = 5;
        // 支持的最大机器id，结果是31 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数)
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        // 支持的最大数据标识id，结果是31
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        // 序列在id中占的位数
        private const int SequenceBits = 12;

        // 数据标识id向左移17位(12+5)
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        // 机器ID向左移12位
        private const int WorkerIdShift = SequenceBits;
        // 时间戳向左移22位(5+5+12)
        private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        // 生成序列的掩码，这里为4095 (0b111111111111=0xfff=4095)
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        // 数据中心ID(0~31)
        public long DatacenterId { get; private set; }
        // 工作机器ID(0~31)
        public long WorkerId { get; private set; }

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }
        public SnowflakeIdHelper(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            }
        }

        readonly object _lock = new Object();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    //exceptionCounter.incr(1);
                    //log.Error("clock is moving backwards.  Rejecting requests until %d.", _lastTimestamp);
                    throw new Exception(String.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
                }
                //如果是同一时间生成的，则进行毫秒内序列
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }

        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}

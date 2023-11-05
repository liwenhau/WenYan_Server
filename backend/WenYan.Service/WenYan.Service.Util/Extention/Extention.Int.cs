namespace WenYan.Service.Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 毫秒转天时分秒
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string FormatTime(this long ms)
        {
            int ss = 1000;
            int mi = ss * 60;
            int hh = mi * 60;
            int dd = hh * 24;

            long day = ms / dd;
            long hour = (ms - day * dd) / hh;
            long minute = (ms - day * dd - hour * hh) / mi;
            long second = (ms - day * dd - hour * hh - minute * mi) / ss;
            long milliSecond = ms - day * dd - hour * hh - minute * mi - second * ss;

            string sDay = day < 10 ? "0" + day : "" + day; //天
            string sHour = hour < 10 ? "0" + hour : "" + hour;//小时
            string sMinute = minute < 10 ? "0" + minute : "" + minute;//分钟
            string sSecond = second < 10 ? "0" + second : "" + second;//秒
            string sMilliSecond = milliSecond < 10 ? "0" + milliSecond : "" + milliSecond;//毫秒
            sMilliSecond = milliSecond < 100 ? "0" + sMilliSecond : "" + sMilliSecond;

            return string.Format("{0} 天 {1} 小时 {2} 分 {3} 秒", sDay, sHour, sMinute, sSecond);
        }
    }
}

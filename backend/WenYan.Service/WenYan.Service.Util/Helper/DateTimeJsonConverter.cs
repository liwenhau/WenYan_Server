using System.Text.Json;
using System.Text.Json.Serialization;

namespace WenYan.Service.Util
{
    /// <summary>
    /// 格式化返回的时间格式
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string format;
        public DateTimeJsonConverter(string _format)
        {
            this.format = _format;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }

    }
}

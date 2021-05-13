using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 3
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenYan.Server.Application.ExtensibleEntityDto
{
    public abstract class EntityDto<TKey>
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public TKey Id
        {
            get;
            set;
        }
    }
}

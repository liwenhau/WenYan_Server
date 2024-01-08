namespace WenYan.Service.Api.Controllers
{
    public class Sys_OrgController : BaseController
    {
        private ISys_OrgBusiness Bus { get; set; }

        public Sys_OrgController(ISys_OrgBusiness bus)
        {
            Bus = bus;
        }

        /// <summary>
        /// 根据组织id获取组织部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Sys_Org> GetAsync(string id)
        {
            return await this.Bus.GetAsync(id);
        }

        /// <summary>
        /// 获取所有组织信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<OrgTreeDto>> GetAllAsync(string name, string status)
        {
            return await this.Bus.GetAllAsync(name, status);
        }

        /// <summary>
        /// 保存组织部门信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveAsync(Sys_Org data)
        {
            this.InitBusEntity(data);
            if (data.ParentId.IsNullOrEmpty())
            {
                data.ParentId = null;
            }
            return await this.Bus.AddOrUpdateAsync(data);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids">数据主键</param>
        /// <returns></returns>
        [HttpDelete]
        public Task<int> DeleteAsync(List<string> ids)
        {
            return this.Bus.DeleteAsync(ids);
        }
    }
}

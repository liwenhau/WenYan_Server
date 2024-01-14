namespace WenYan.Service.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [FormatResponse]
    public class BaseController : ControllerBase
    {
        protected void InitBusEntity<T>(T entity) where T : BusEntity
        {
            if (entity.Id.IsNullOrEmpty())
            {
                var IdSvc = HttpContext.RequestServices.GetService<IIdService>();
                entity.Id = IdSvc.GetId();
            }
            if (entity.CreateUserId.IsNullOrEmpty() || entity.ModifyUserId.IsNullOrEmpty())
            {
                var opSvc = HttpContext.RequestServices.GetService<IOperator>();
                entity.CreateUserId = entity.CreateUserId.IsNullOrEmpty() ? opSvc.UserId : entity.CreateUserId;
                entity.ModifyUserId = entity.ModifyUserId.IsNullOrEmpty() ? opSvc.UserId : entity.ModifyUserId;
            }
            if (entity.CreateTime == default) entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
        }
    }
}

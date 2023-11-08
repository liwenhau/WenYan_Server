using System.Text;

namespace WenYan.Service.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [FormatResponse]
    public class BaseController : ControllerBase
    {

    }
}

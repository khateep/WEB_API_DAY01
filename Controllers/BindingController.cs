using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API_DAY01.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //if promitive DT (int string flaot)=> RouteData | Q.String
    // if Complex DT => Request Body
    public class BindingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }
    }
}

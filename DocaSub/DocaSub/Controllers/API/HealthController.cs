using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocaSub.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new {status = "Heathly"});
        }
    }
}

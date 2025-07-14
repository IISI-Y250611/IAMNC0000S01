using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Security.Claims;

namespace IAMNC0000S03.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected static readonly Logger _nLogger = LogManager.GetCurrentClassLogger();
        protected readonly ILogger<BaseApiController> _logger;

        public BaseApiController(ILogger<BaseApiController> logger)
        {
            _logger = logger;
        }

        protected string CurrentUserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
        protected string CurrentFullName => User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
        protected string CurrentTenantId => Request.Headers["TrnantId"].ToString();
        protected string AccessToken => Request.Headers["Authorization"].ToString();

        protected IActionResult ApiResult<T>(T result)
        {
            if (result == null)
            {
                return BadRequest(new { message = "Data not found" });
            }
            return Ok(result);
        }
    }
}
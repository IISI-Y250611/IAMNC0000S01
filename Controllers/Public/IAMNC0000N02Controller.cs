using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Business.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IAMNC0000S03.Controllers.Public
{
    [Route("api/")]
    [ApiController]

    public class IAMNC0000N02Controller : BaseApiController
    {
        private readonly IIAMNC0000N02Service _service;

        private static readonly ActivitySource activitySource = new("IAMNC0000N02Controller");

        public IAMNC0000N02Controller(IIAMNC0000N02Service service, ILogger<BaseApiController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpPost]
        [Route("GetBullientData")]
        public async Task<IActionResult> GetBullientData([FromBody] BullientDataQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-GetBullientData");
            _nLogger.Info("公告事項API - GetBullientData");
            var result = await _service.GetBullientData(query);
            return ApiResult(result);
        }

        [HttpPost]
        [Route("GetPopNotificationsData")]
        public async Task<IActionResult> GetPopNotificationsData([FromBody] PopNotificationsQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-GetPopNotificationsData");
            _nLogger.Info("公告事項API - GetPopNotificationsData");
            var result = await _service.GetPopNotificationsData(query);
            return ApiResult(result);
        }
    }
}
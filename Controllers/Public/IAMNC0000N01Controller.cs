using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Business.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IAMNC0000S03.Controllers.Public
{
    [Route("api/")]
    [ApiController]

    public class IAMNC0000Controller : BaseApiController
    {
        private readonly IIAMNC0000Service _service;

        private static readonly ActivitySource activitySource = new("IAMNC0000Controller");

        public IAMNC0000Controller(IIAMNC0000Service service, ILogger<BaseApiController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpPost]
        [Route("users/RoleList")]
        public async Task<IActionResult> GetRoleList([FromBody] UserRoleQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-GetUserInformation");
            _nLogger.Info("收到 API 請求 讀取使用者帳號名稱與角色清單 - GetUserInformation");

            if (string.IsNullOrWhiteSpace(query.UserId))
            {
                ModelState.AddModelError("Message", "使用者代號不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.SystemCode))
            {
                ModelState.AddModelError("Message", "應用系統代碼不可空白");
                return BadRequest(ModelState);
            }

            var isExist = await _service.CheckUserExists(query.UserId, query.BranchCode);
            if (!isExist)
            {
                return Ok(new { Result = "1", Message = "查無使用者代號" });
            }

            var result = await _service.GetRoleList(query.UserId, query.SystemCode, query.BranchCode);
            return ApiResult(result);
        }

        /// <summary>
        /// 讀取署內組織單位清單
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("nhi/OrganizationList")]
        public async Task<IActionResult> GetOrganizationList([FromBody] BaseQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-GetUserInformation");
            _nLogger.Info("收到 API 請求 讀取署內組織單位清單 - GetOrganizationList");

            var result = await _service.GetOrganizationList(query.BranchCode);
            return ApiResult(result);
        }

        /// <summary>
        /// 讀取人員相關資料
        /// </summary>
        /// <param name="roleKind"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/UserInformation")]
        public async Task<IActionResult> GetUserInformation([FromBody] OrgQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-GetUserInformation");
            _nLogger.Info("收到 API 請求 讀取人員相關資訊資料 - GetUserInformation");

            if (!string.IsNullOrWhiteSpace(query.OrgId))
            {
                var isExists = await _service.CheckOrgExists(query.OrgId);
                if (!isExists)
                {
                    return Ok(new { Result = "1", Message = "查無該組室別" });
                }
            }

            var result = await _service.GetUserInformation(query.BranchCode, query.OrgId);
            return ApiResult(result);
        }

        /// <summary>
        /// 讀取人員角色與功能權利資料
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/UserRolesAndFunctions")]
        public async Task<IActionResult> GetUserRolesAndFunctions([FromBody] UserRoleFunQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-CheckFunction");
            _nLogger.Info("收到 API 請求 檢查使用者功能權利 - CheckFunction");

            if (string.IsNullOrWhiteSpace(query.BranchCode))
            {
                ModelState.AddModelError("Message", "授權業務組別不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.UserId))
            {
                ModelState.AddModelError("Message", "使用者代號不可空白");
                return BadRequest(ModelState);
            }

            var isExist = await _service.CheckUserExists(query.UserId, query.BranchCode);
            if (!isExist)
            {
                return Ok(new { Result = "1", Message = "查無使用者代號" });
            }

            var result = await _service.GetUserRolesAndFunctions(query);
            return ApiResult(result);
        }


        /// <summary>
        /// 檢查使用者功能權利
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("users/CheckFunction")]
        public async Task<IActionResult> CheckFunction([FromBody] FunctionQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-CheckFunction");
            _nLogger.Info("收到 API 請求 檢查使用者功能權利 - CheckFunction");

            if (string.IsNullOrWhiteSpace(query.FunctionId))
            {
                ModelState.AddModelError("Message", "功能代碼不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.UserId))
            {
                ModelState.AddModelError("Message", "使用者代碼不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.SystemCode))
            {
                ModelState.AddModelError("Message", "應用系統代碼不可空白");
                return BadRequest(ModelState);
            }

            var isExist = await _service.CheckUserExists(query.UserId, query.BranchCode);
            if (!isExist)
            {
                return Ok(new { Result = "1", Message = "查無使用者代號" });
            }

            var result = await _service.CheckFunction(query.BranchCode, query.SystemCode, query.UserId, query.FunctionId);
            return ApiResult(result);
        }

        /// <summary>
        /// 檢查使用者角色權利
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/CheckRole")]
        public async Task<IActionResult> CheckRole([FromBody] RoleQueryDto query)
        {
            using var activity = activitySource.StartActivity("API-CheckRole");
            _nLogger.Info("收到 API 請求 檢查使用者角色權利 - CheckRole");

            if (string.IsNullOrWhiteSpace(query.RoleId))
            {
                ModelState.AddModelError("Message", "角色代碼不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.UserId))
            {
                ModelState.AddModelError("Message", "使用者不可空白");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(query.SystemCode))
            {
                ModelState.AddModelError("Message", "應用系統代碼不可空白");
                return BadRequest(ModelState);
            }

            var isExist = await _service.CheckUserExists(query.UserId, query.BranchCode);
            if (!isExist)
            {
                return Ok( new { Result = "1", Message = "查無使用者代號" });
            }

            var result = await _service.CheckRole(query.BranchCode, query.SystemCode, query.UserId, query.RoleId);
            return ApiResult(result);
        }
    }
}
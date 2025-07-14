namespace IAMNC0000S03.Business.Models
{
    public class UserRoleFunDto
    {
        /// <summary>
        /// 業務組別代號
        /// </summary>
        public required string BranchCode { get; set; }

        /// <summary>
        /// 使用者代號
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// 使用者電子郵件
        /// </summary>
        public string? UserEmail { get; set; }

        /// <summary>
        /// 組室單位代號
        /// </summary>
        public required string OrgId { get; set; }

        /// <summary>
        /// 組室單位名稱
        /// </summary>
        public required string OrgName { get; set; }

        /// <summary>
        /// 使用者類別，0-正職、1-署內外包人員、8-專審醫師、9-其他外部人員
        /// </summary>
        public required string UserKind { get; set; }

        /// <summary>
        /// 使用者主管代號
        /// </summary>
        public string? MgrUserId { get; set; }

        /// <summary>
        /// 白名單註記(Y/N)
        /// </summary>
        public string? WhiteListMark { get; set; }

        /// <summary>
        /// 白名單有效起日(格式:YYYY-MM-DD)
        /// </summary>
        public string? WhiteStartDate { get; set; }

        /// <summary>
        /// 白名單有效迄日(格式:YYYY-MM-DD)
        /// </summary>
        public string? WhiteEndDate { get; set; }

        /// <summary>
        /// 電子郵件認證月累次數(最大整數3位)
        /// </summary>
        public decimal? AuthEmailTimes { get; set; }

        /// <summary>
        /// 使用者密碼(SHA256加密)
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// 使用者帳號之有效狀態(Y/N)
        /// </summary>
        public required string IdEnable { get; set; }

        /// <summary>
        /// 使用者帳號鎖定狀態(Y/N)
        /// </summary>
        public string? IdLockStatus { get; set; }

        /// <summary>
        /// 須變更密碼(Y/N)
        /// </summary>
        public required string NeedChangePwd { get; set; }

        /// <summary>
        /// (人員)有效起日(格式:YYYY-MM-DD)
        /// </summary>
        public required string ValidStartDate { get; set; }

        /// <summary>
        /// (人員)有效迄日(格式:YYYY-MM-DD)
        /// </summary>
        public required string ValidEndDate { get; set; }

        /// <summary>
        /// 最近一次密碼變更日期時間(格式:YYYY-MM-DDTHH24:MI:SS)
        /// </summary>
        public string? LastChangePwdDate { get; set; }

        /// <summary>
        /// 最近一次成功登入日期時間(格式:YYYY-MM-DDTHH24:MI:SS)
        /// </summary>
        public string? LastSuccessLoginDate { get; set; }

        /// <summary>
        /// 最近一次失敗登入日期時間(格式:YYYY-MM-DDTHH24:MI:SS)
        /// </summary>
        public string? LastFailLoginDate { get; set; }

        /// <summary>
        /// 最近一次鎖定日期時間(格式:YYYY-MM-DDTHH24:MI:SS)
        /// </summary>
        public string? LastLockDate { get; set; }

        /// <summary>
        /// 失敗登入次數(自最近成功登入起算) (最大整數4位)
        /// </summary>
        public decimal? FailLoginCount { get; set; }

        /// <summary>
        /// 密碼連續失敗次數(最大整數4位)
        /// </summary>
        public decimal? ContinueFaultCount { get; set; }

        /// <summary>
        /// 密碼非連續失敗次數(最大整數4位)
        /// </summary>
        public required decimal NonContinueFaultCount { get; set; }

        /// <summary>
        /// 密碼變更次數 (計數用) (最大整數4位)
        /// </summary>
        public required decimal PwdChangeCounter { get; set; }

        /// <summary>
        /// 密碼永久正確(Y/N)
        /// </summary>
        public required string PwdValidityPermanent { get; set; }

        public required List<RoleDto> RolesData { get; set; }
        public required List<FunctionDto> FunctionsData { get; set; }
    }
}
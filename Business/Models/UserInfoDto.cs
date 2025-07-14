namespace IAMNC0000S03.Business.Models
{
    /// <summary>
    /// 資料帶回
    /// </summary>
    public class UserInfoDto
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

        public required List<SupportBranchDto> SupportBranchData { get; set; }


        public class SupportBranchDto
        {
            public required string SupportBranchCode { get; set; }
        }
    }
}
namespace IAMNC0000S03.Business.Models
{
    /// <summary>
    /// 資料帶回
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// 角色代號
        /// </summary>
        public required string RoleId { get; set; }

        /// <summary>
        /// 角色名稱
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// 角色類別(三代醫療系統為子系統代號)
        /// </summary>
        public string? RoleKind { get; set; }

        /// <summary>
        /// 角色說明
        /// </summary>
        public string? RoleDescription { get; set; }

        /// <summary>
        /// 排序編號
        /// </summary>
        public string? SortOrdere { get; set; }

        /// <summary>
        /// 角色設定管理人業務組別
        /// </summary>
        public string? MgrBranchCode { get; set; }

        /// <summary>
        /// 角色設定管理人組室別
        /// </summary>
        public string? MgrOrgId { get; set; }
    }
}
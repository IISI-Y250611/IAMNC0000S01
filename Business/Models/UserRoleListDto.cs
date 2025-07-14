namespace IAMNC0000S03.Business.Models
{
    public class UserRoleListDto
    {
        /// <summary>
        /// 使用者代號
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<RoleList>? RoleList { get; set; }
    }

    public class RoleList
    {
        public string? RoleId { get; set; }
    }
}
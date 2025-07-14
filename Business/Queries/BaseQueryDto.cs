using System.ComponentModel.DataAnnotations;

namespace IAMNC0000S03.Business.Queries
{
    //查詢參數  
    public class BaseQueryDto
    {
        /// <summary>  
        /// 業務組別代碼 0-6或空值  
        /// </summary>  
        [RegularExpression("^[0-6]?$", ErrorMessage = "BranchCode 必須是 0-6 的數字或空值")]
        public string? BranchCode { get; set; }
    }

    public class UserRoleQueryDto : BaseQueryDto
    {
        /// <summary>  
        /// 使用者代號  
        /// </summary>  
        public required string UserId { get; set; }

        /// <summary>  
        /// 應用系統代號  
        /// </summary>  
        public required string SystemCode { get; set; }
    }

    public class UserRoleFunQueryDto : BaseQueryDto
    {
        /// <summary>  
        /// 使用者代號  
        /// </summary>  
        public required string UserId { get; set; }

        /// <summary>  
        /// 應用系統代號  
        /// </summary>  
        public required string SystemCode { get; set; }
    }

    public class OrgQueryDto : BaseQueryDto
    {
        /// <summary>
        /// 組室別代號
        /// </summary>
        public string? OrgId { get; set; }
    }


    public class FunctionQueryDto : BaseQueryDto
    {
        /// <summary>  
        /// 使用者代號  
        /// </summary>  
        public required string UserId { get; set; }

        /// <summary>  
        /// 應用系統代號  
        /// </summary>  
        public required string SystemCode { get; set; }

        /// <summary>
        /// 功能權利代號
        /// </summary>
        public required string FunctionId { get; set; }
    }

    public class RoleQueryDto : BaseQueryDto
    {
        /// <summary>  
        /// 使用者代號  
        /// </summary>  
        public required string UserId { get; set; }

        /// <summary>  
        /// 應用系統代號  
        /// </summary>  
        public required string SystemCode { get; set; }

        /// <summary>
        /// 角色代號
        /// </summary>
        public required string RoleId { get; set; }
    }
}
namespace IAMNC0000S03.Business.Models
{
    public class OrgInfoDto
    {
        /// <summary>
        /// 業務組別代號
        /// </summary>
        public required string BranchCode { get; set; }

        /// <summary>
        /// 組室單位代號
        /// </summary>
        public required string OrgId { get; set; }

        /// <summary>
        /// 組室單位名稱
        /// </summary>
        public required string OrgName { get; set; }
    }
}

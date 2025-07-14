namespace IAMNC0000S03.Domain.Entity
{
    public class UserEntity
    {
        public required string BRANCH_CODE { get; set; }
        public required string USER_ID { get; set; }
        public required string USER_NAME { get; set; }
        public required string USER_KIND { get; set; }
        public required string OU_ID { get; set; }
        public required string OU_NAME { get; set; }
        public string? MGR_USER_ID { get; set; }
        public string? USER_EMAIL { get; set; }
        public string? WLIST_MARK { get; set; }
        public DateTime? WLIST_S_DATE { get; set; }
        public DateTime? WLIST_E_DATE { get; set; }
        public decimal? AUTH_EMAIL_TIMES { get; set; }
        public required string PASSWORD { get; set; }
        public required string ID_ENABLE { get; set; }
        public string? ID_LOCK_STATE { get; set; }
        public required string NEED_CHANPSWD { get; set; }
        public required DateTime VALID_S_DATE { get; set; }
        public required DateTime VALID_E_DATE { get; set; }
        public DateTime? LAST_CHANPSWD { get; set; }
        public DateTime? LAST_SUCCLOGIN { get; set; }
        public DateTime? LAST_FAILLOGIN { get; set; }
        public DateTime? LAST_LOCK_DATE { get; set; }
        public required decimal CNT_FAILLOGIN { get; set; }
        public required decimal CON_FAULT { get; set; }
        public required decimal NONCON_FAULT { get; set; }
        public required decimal COUNTER { get; set; }
        public required string PSWD_ALLRIGHT { get; set; }
        //
        public string? USER_TYPE { get; set; }
        public string? USER_DESC { get; set; }
        public string? ID_AES { get; set; }
        public string? EMP_ID { get; set; }
        public string? USER_NICKNAME { get; set; }
        public List<SupportBranchEnity>? SupportBranchData { get; set; } = new();


        public class SupportBranchEnity
        {
            public required string USER_ID { get; set; }
            public string? SUP_BRANCH_CODE { get; set; }
        }
    }
}
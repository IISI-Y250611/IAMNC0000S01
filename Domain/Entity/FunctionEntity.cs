namespace IAMNC0000S03.Domain.Entity
{
    public class FunctionEntity
    {
        public required string SYSTEM_CODE { get; set; }
        public required string FUN_ID { get; set; }
        public required string FUN_NAME { get; set; }
        public required string FUN_TYPE { get; set; }
        public required string FUN_VISIBLE { get; set; }
        public string? EXP_PATH { get; set; }
        public string? PRG_PATH { get; set; }
        public required string PARENT_FUN_ID { get; set; }
        public string? FUN_DESC { get; set; }
        public string? SORT_ORDER { get; set; }
        public required string BUS_CODE { get; set; }
        public required string FUN_CATEGORY { get; set; }
        public required string SENSITIVE_FUN_FLAG { get; set; }
        public required string IS_AUTH_FUNCTION { get; set; }
        public string? EMAIL { get; set; }
        public string? FUN_COLOR { get; set; }
        public string? FUN_ICON { get; set; }
        public string? PRG_ACTION { get; set; }
        public string? IS_SINGLE_AUTH { get; set; }
        public string? TARGET_USER_MARK { get; set; }
        public string? REMOTE_CALL_FLAG { get; set; }
        public string? MGR_OU_ID { get; set; }
        public required DateTime CREATE_DATE { get; set; }
        public required string CREATE_USER_ID { get; set; }
        public required DateTime TXT_DATE { get; set; }
        public required string TXT_USER_ID { get; set; }
    }
}

public class RoleEntity
{
    public required string ROLE_ID { get; set; }
    public required string ROLE_NAME { get; set; }
    public string? ROLE_KIND { get; set; }
    public string? ROLE_DESC { get; set; }
    public string? SORT_ORDER { get; set; }
    public string? MGR_BRANCH_CODE { get; set; }
    public string? MGR_OU_ID { get; set; }
    public required string HAS_SENSITIVE_FUN_FLAG { get; set; }
    public string? ROLE_TYPE { get; set; }
}
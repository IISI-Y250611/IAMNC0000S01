using IAMNC0000S03.Business.Queries;
using IAMNC0000S03.Domain.Entity;
using static IAMNC0000S03.Domain.Entity.UserEntity;

namespace IAMNC0000S03.Repository
{
    public interface IIAMNC0000Repository
    {
        Task<IEnumerable<RoleEntity>> GetRoleList(string userId, string systemCode, string? branchCode);
        Task<IEnumerable<OrgEntity>> GetOrganizationList(string? branchCode);
        Task<UserEntity> GetDeatilUserInfo(string branchCode, string userId);
        Task<IEnumerable<RoleEntity>> GetUserAssignedRoles(string systemCode, string branchCode, string userId);
        Task<IEnumerable<FunctionEntity>> GetUserAssignedFunctions(string systemCode, string branchCode, string userId);
        Task<IEnumerable<UserEntity>> GetUserBaseInfo(string? branchCode = null, string? userId = null, string? orgId = null);
        Task<IEnumerable<SupportBranchEnity>> GetSuppourtCode(string? orgId, string? branchCode);
        Task<bool> CheckFunction(string? branchCode, string systemCode, string userId, string functionId);
        Task<bool> CheckRole(string? branchCode, string systemCode, string userId, string roleId);
        Task<bool> CheckUserExists(string userId, string? branchCode);
        Task<bool> CheckOrgExists(string orgId);
    }
}
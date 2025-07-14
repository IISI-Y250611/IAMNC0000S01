using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using static IAMNC0000S03.Business.Models.ApiResponse;

namespace IAMNC0000S03.Business.Interfaces
{
    public interface IIAMNC0000Service
    {
        Task<ApiResponsewithData<UserRoleListDto>> GetRoleList(string userId, string systemCode, string? branchCode);
        Task<ApiResponsewithData<OrgInfoDto>> GetOrganizationList(string? branchCode);
        Task<ApiResponsewithData<UserInfoDto>> GetUserInformation(string? branchCode, string? orgId);
        Task<ApiResponsewithData<UserRoleFunDto>> GetUserRolesAndFunctions(UserRoleFunQueryDto query);
        Task<ApiResponseWithFlag> CheckFunction(string? branchCode, string systemCode, string userId, string functionId);
        Task<ApiResponseWithFlag> CheckRole(string? branchCode, string systemCode, string userId, string roleId);
        Task<bool> CheckUserExists (string userId, string? branchCode);
        Task<bool> CheckOrgExists(string orgId);
    }
}
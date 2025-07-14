using IAMNC0000S03.Business.EF.Mappers;
using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using IAMNC0000S03.Repository;
using Sprache;
using static IAMNC0000S03.Business.Models.ApiResponse;

namespace IAMNC0000S03.Business.EF.Services
{
    public class IAMNC0000Service : IIAMNC0000Service
    {
        private readonly IIAMNC0000Repository _repository;

        public IAMNC0000Service(IIAMNC0000Repository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="systemCode"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<UserRoleListDto>> GetRoleList(string userId, string systemCode, string? branchCode)
        {
            var userInfoTask = _repository.GetUserBaseInfo(branchCode: branchCode, userId: userId);
            var roleListTask = _repository.GetRoleList(userId, systemCode, branchCode);

            await Task.WhenAll(userInfoTask, roleListTask);

            var userInfo = userInfoTask.Result.FirstOrDefault();
            var roleList = roleListTask.Result;

            if (userInfo != null && roleList.Count() != 0)
            {
                return new ApiResponsewithData<UserRoleListDto>()
                {
                    Data = new List<UserRoleListDto> { UserRoleListMapper.MapFrom(userInfo, roleList) }
                };
            }

            return new ApiResponsewithData<UserRoleListDto>("查無該使用者代號")
            {
                Data = new List<UserRoleListDto> { UserRoleListMapper.MapFrom(userInfo, roleList) }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<OrgInfoDto>> GetOrganizationList(string? branchCode)
        {
            var orgInfoList = await _repository.GetOrganizationList(branchCode);

            if (orgInfoList.Any())
            {
                return new ApiResponsewithData<OrgInfoDto>()
                {
                    Data = OrgInfoMapper.MapFrom(orgInfoList)
                };
            }

            return new ApiResponsewithData<OrgInfoDto>("查無組室別單位資料");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<UserInfoDto>> GetUserInformation(string? branchCode, string? orgId)
        {
            var userListTask = _repository.GetUserBaseInfo(branchCode: branchCode, orgId: orgId);
            var supportCodeTask = _repository.GetSuppourtCode(orgId, branchCode);

            await Task.WhenAll(userListTask, supportCodeTask);
            if (userListTask.Result.Any())
            {
                return new ApiResponsewithData<UserInfoDto>()
                {
                    Data = UserSupportMapper.MapFrom(userListTask.Result, supportCodeTask.Result)
                };
            }

            return new ApiResponsewithData<UserInfoDto>("查無組室別使用者相關資料");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<UserRoleFunDto>> GetUserRolesAndFunctions(UserRoleFunQueryDto query)
        {
            var userInfoTask = _repository.GetDeatilUserInfo(query.BranchCode, query.UserId);
            var roleDataTask = _repository.GetUserAssignedRoles(query.SystemCode, query.BranchCode, query.UserId);
            var functionDataTask = _repository.GetUserAssignedFunctions(query.SystemCode, query.BranchCode, query.UserId);

            await Task.WhenAll(userInfoTask, roleDataTask, functionDataTask);

            var roleData = roleDataTask.Result?.Select(RoleDtoMapper.MapFrom).ToList();
            var functionData = functionDataTask.Result?.Select(FunctionDtoMapper.MapFrom).ToList();

            bool isRolesEmpty = roleData == null || !roleData.Any();
            bool isFunctionsEmpty = functionData == null || !functionData.Any();

            if (isRolesEmpty && isFunctionsEmpty)
            {
                return new ApiResponsewithData<UserRoleFunDto>("查無該使用者已授予之角色或功能權利相關資料");
            }

            return new ApiResponsewithData<UserRoleFunDto>
            {
                Data = new List<UserRoleFunDto> { RoleFunMapper.MapFrom(userInfoTask.Result, roleData, functionData) }
            };
        }

        public async Task<ApiResponseWithFlag> CheckFunction(string? branchCode, string systemCode, string userId, string functionId)
        {
            bool isExist = await _repository.CheckFunction(branchCode, systemCode, userId, functionId);

            return new ApiResponseWithFlag
            {
                Message = isExist ? "success" : "查無該使用者已授權之功能權利代號",
                IsExist = isExist
            };
        }

        public async Task<ApiResponseWithFlag> CheckRole(string? branchCode, string systemCode, string userId, string roleId)
        {
            bool isExist = await _repository.CheckRole(branchCode, systemCode, userId, roleId);

            return new ApiResponseWithFlag
            {
                Message = isExist ? "success" : "查無該使用者已授予之角色代號",
                IsExist = isExist
            };
        }

        public async Task<bool> CheckUserExists(string userId, string? branchCode)
        {
            return await _repository.CheckUserExists(userId, branchCode);
        }

        public async Task<bool> CheckOrgExists(string orgId)
        {
            return await _repository.CheckOrgExists(orgId);
        }
    }
}
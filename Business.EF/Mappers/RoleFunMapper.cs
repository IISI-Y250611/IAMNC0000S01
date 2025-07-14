using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public static class RoleFunMapper
    {
        public static UserRoleFunDto MapFrom(UserEntity entity, List<RoleDto>? roles, List<FunctionDto>? functions)
        {
            return new UserRoleFunDto
            {
                // BaseUserInfoDto
                BranchCode = entity.BRANCH_CODE,
                UserId = entity.USER_ID,
                UserName = entity.USER_NAME,
                UserKind = entity.USER_KIND,
                OrgId = entity.OU_ID,
                OrgName = entity.OU_NAME,

                // UserDetailDto
                MgrUserId = entity.MGR_USER_ID,
                UserEmail = entity.USER_EMAIL,
                WhiteListMark = entity.WLIST_MARK,
                WhiteStartDate = entity.WLIST_S_DATE?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                WhiteEndDate = entity.WLIST_E_DATE?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                AuthEmailTimes = entity.AUTH_EMAIL_TIMES,
                Password = entity.PASSWORD,
                IdEnable = entity.ID_ENABLE,
                IdLockStatus = entity.ID_LOCK_STATE,
                NeedChangePwd = entity.NEED_CHANPSWD,
                ValidStartDate = entity.VALID_S_DATE.ToString("yyyy-MM-dd"),
                ValidEndDate = entity.VALID_E_DATE.ToString("yyyy-MM-dd"),
                LastChangePwdDate = entity.LAST_CHANPSWD?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                LastSuccessLoginDate = entity.LAST_SUCCLOGIN?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                LastFailLoginDate = entity.LAST_FAILLOGIN?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                LastLockDate = entity.LAST_LOCK_DATE?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
                FailLoginCount = entity.CNT_FAILLOGIN,
                ContinueFaultCount = entity.CON_FAULT,
                NonContinueFaultCount = entity.NONCON_FAULT,
                PwdChangeCounter = entity.COUNTER,
                PwdValidityPermanent = entity.PSWD_ALLRIGHT,

                //
                RolesData = roles ?? new List<RoleDto>(),
                FunctionsData = functions ?? new List<FunctionDto>()
            };
        }
    }
}

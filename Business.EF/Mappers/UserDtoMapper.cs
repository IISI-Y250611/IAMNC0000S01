using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class UserDtoMapper
    {
        public static UserInfoDto MapFrom(UserEntity entity) => new()
        {
            BranchCode = entity.BRANCH_CODE,
            UserId = entity.USER_ID,
            UserName = entity.USER_NAME,
            OrgId = entity.OU_ID,
            OrgName = entity.OU_NAME,
            UserEmail = entity.USER_EMAIL,
            SupportBranchData = entity.SupportBranchData?.Select(x => new UserInfoDto.SupportBranchDto
            {
                SupportBranchCode = x.SUP_BRANCH_CODE ?? string.Empty
            }).ToList() ?? new List<UserInfoDto.SupportBranchDto>()
        };
    }
}
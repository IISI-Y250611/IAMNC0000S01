using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;
using static IAMNC0000S03.Business.Models.UserInfoDto;
using static IAMNC0000S03.Domain.Entity.UserEntity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class UserSupportMapper
    {
        public static List<UserInfoDto> MapFrom(IEnumerable<UserEntity> entity, IEnumerable<SupportBranchEnity>? supportEntity)
        {
            var supportMap = (supportEntity ?? Enumerable.Empty<SupportBranchEnity>())
                .GroupBy(x => x.USER_ID)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<UserInfoDto>();

            foreach (var user in entity)
            {
                supportMap.TryGetValue(user.USER_ID, out var userSupports);
                var filtered = userSupports?.Where(s => s.SUP_BRANCH_CODE != user.BRANCH_CODE).ToList();

                var dto = new UserInfoDto
                {
                    UserId = user.USER_ID,
                    UserName = user.USER_NAME,
                    UserEmail = user.USER_EMAIL,
                    BranchCode = user.BRANCH_CODE,
                    OrgId = user.OU_ID,
                    OrgName = user.OU_NAME,
                    SupportBranchData = filtered?.Select(s => new SupportBranchDto
                    {
                        SupportBranchCode = s.SUP_BRANCH_CODE ?? string.Empty
                    }).ToList() ?? new List<SupportBranchDto>()
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
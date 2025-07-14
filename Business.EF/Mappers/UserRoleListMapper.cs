using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class UserRoleListMapper
    {
        public static UserRoleListDto MapFrom(UserEntity entity, IEnumerable<RoleEntity>? roles)
        {
            if (roles == null || !roles.Any())
            {
                return new UserRoleListDto
                {
                    UserId = entity.USER_ID,
                    UserName = string.Empty,
                    RoleList = new List<RoleList>()
                };
            }

            return new UserRoleListDto
            {
                UserId = entity.USER_ID,
                UserName = entity.USER_NAME,
                RoleList = roles.Select(x => new RoleList
                {
                    RoleId = x.ROLE_ID
                }).ToList()
            };
        }
    }
}
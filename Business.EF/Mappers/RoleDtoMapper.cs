using IAMNC0000S03.Business.Models;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class RoleDtoMapper
    {
        public static RoleDto MapFrom(RoleEntity entity) => new()
        {
            RoleId = entity.ROLE_ID,
            RoleName = entity.ROLE_NAME,
            RoleKind = entity.ROLE_KIND,
            RoleDescription = entity.ROLE_DESC,
            SortOrdere = entity.SORT_ORDER,
            MgrBranchCode = entity.MGR_BRANCH_CODE,
            MgrOrgId = entity.MGR_OU_ID
        };
    }
}
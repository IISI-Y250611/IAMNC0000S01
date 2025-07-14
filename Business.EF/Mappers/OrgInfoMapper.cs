using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class OrgInfoMapper
    {
        public static List<OrgInfoDto> MapFrom(IEnumerable<OrgEntity> entity)
        {
            var result = new List<OrgInfoDto>();

            foreach (var org in entity)
            {
                var dto = new OrgInfoDto()
                {
                    OrgId = org.OU_ID,
                    OrgName = org.OU_NAME,
                    BranchCode = org.BRANCH_CODE
                };

                result.Add(dto);
            };

            return result;
        }
    }
}
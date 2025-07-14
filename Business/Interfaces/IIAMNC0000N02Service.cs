using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using static IAMNC0000S03.Business.Models.ApiResponse;

namespace IAMNC0000S03.Business.Interfaces
{
    public interface IIAMNC0000N02Service
    {
        Task<ApiResponsewithData<BullientDataDto>> GetBullientData(BullientDataQueryDto query);
        Task<ApiResponsewithData<PopNotificationsDataDto>> GetPopNotificationsData(PopNotificationsQueryDto query);
    }
}
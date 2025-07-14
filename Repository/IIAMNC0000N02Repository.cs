using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using IAMNC0000S03.Domain.Entity;
using static IAMNC0000S03.Domain.Entity.UserEntity;

namespace IAMNC0000S03.Repository
{
    public interface IIAMNC0000N02Repository
    {
        Task<IEnumerable<BullientDataDto>> GetBullientData(BullientDataQueryDto query);
        Task<IEnumerable<AttachmentFile>> GetAttachmentFile(int MsgSeq);
        Task<IEnumerable<PopNotificationsDataDto>> GetPopNotificationsData(PopNotificationsQueryDto query);
    }
}
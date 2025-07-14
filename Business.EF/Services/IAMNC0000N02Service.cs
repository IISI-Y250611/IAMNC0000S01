using IAMNC0000S03.Business.EF.Mappers;
using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using IAMNC0000S03.Repository;
using Sprache;
using static IAMNC0000S03.Business.Models.ApiResponse;

namespace IAMNC0000N02S03.Business.EF.Services
{
    public class IAMNC0000N02Service : IIAMNC0000N02Service
    {
        private readonly IIAMNC0000N02Repository _repository;

        public IAMNC0000N02Service(IIAMNC0000N02Repository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BullientDataDto"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<BullientDataDto>> GetBullientData(BullientDataQueryDto query)
        {
            var bullientDataDtoList = await _repository.GetBullientData(query);


            foreach (var bullientDataDto in bullientDataDtoList)
            {
                //取得附件
                bullientDataDto.AttachmentFiles = (List<AttachmentFile>?)await _repository.GetAttachmentFile(bullientDataDto.MsgSeq);
            }

            return new ApiResponsewithData<BullientDataDto>()
            {
                Data = (List<BullientDataDto>)bullientDataDtoList
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ApiResponsewithData<PopNotificationsDataDto>> GetPopNotificationsData(PopNotificationsQueryDto query)
        {
            var notificationsDataDtoList = await _repository.GetPopNotificationsData(query);


            return new ApiResponsewithData<PopNotificationsDataDto>()
            {
                Data = (List<PopNotificationsDataDto>)notificationsDataDtoList
            };
        }
    }
}
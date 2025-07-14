using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Domain.Entity;

namespace IAMNC0000S03.Business.EF.Mappers
{
    public class FunctionDtoMapper
    {
        public static FunctionDto MapFrom(FunctionEntity entity) => new()
        {
            FunctionId = entity.FUN_ID,
            FunctionName = entity.FUN_NAME,
            FunctionVisible = entity.FUN_VISIBLE,
            ProgramPath = entity.PRG_PATH,
            ParentFunctionId = entity.PARENT_FUN_ID,
            FunctionDescription = entity.FUN_DESC,
            SortOrder = entity.SORT_ORDER,
            BusCode = entity.BUS_CODE,
            FunctionCategory = entity.FUN_CATEGORY,
            SensitiveFunctionFlag = entity.SENSITIVE_FUN_FLAG,
            FunctionColor = entity.FUN_COLOR,
            FunctionIcon = entity.FUN_ICON,
            IsSingleAuth = entity.IS_SINGLE_AUTH,
            TargetUserMark = entity.TARGET_USER_MARK,
            RemoteCallFlag = entity.REMOTE_CALL_FLAG
        };
    }
}
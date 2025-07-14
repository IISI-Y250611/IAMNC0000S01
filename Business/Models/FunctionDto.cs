namespace IAMNC0000S03.Business.Models
{
    public class FunctionDto
    {
        /// <summary>
        /// 功能權利代號
        /// </summary>
        public required string FunctionId { get; set; }

        /// <summary>
        /// 功能權利名稱
        /// </summary>
        public required string FunctionName { get; set; }

        /// <summary>
        /// 功能顯示註記Y/N
        /// </summary>
        public required string FunctionVisible { get; set; }

        /// <summary>
        /// 功能執行目錄 
        /// </summary>
        public string? ProgramPath { get; set; }

        /// <summary>
        /// 父功能代號 
        /// </summary>
        public string? ParentFunctionId { get; set; }

        /// <summary>
        /// 功能權利說明
        /// </summary>
        public string? FunctionDescription { get; set; }

        /// <summary>
        /// 排序編號
        /// </summary>
        public string? SortOrder { get; set; }

        /// <summary>
        /// 子系統代號(三代醫療資訊系統)
        /// </summary>
        public required string BusCode { get; set; }

        /// <summary>
        /// 功能權利類別
        /// </summary>
        public required string FunctionCategory { get; set; }

        /// <summary>
        /// 機敏功能作業註記
        /// </summary>
        public required string SensitiveFunctionFlag { get; set; }

        /// <summary>
        /// 功能作業顯示顏色
        /// </summary>
        public string? FunctionColor { get; set; }

        /// <summary>
        /// 功能作業顯示圖示
        /// </summary>
        public string? FunctionIcon { get; set; }

        /// <summary>
        /// 是否為可單一授權功能
        /// </summary>
        public string? IsSingleAuth { get; set; }

        /// <summary>
        /// 使用對象註記
        /// </summary>
        public string? TargetUserMark { get; set; }

        /// <summary>
        /// 遠端程式呼叫標示(IntraCallRemote用)。
        /// </summary>
        public string? RemoteCallFlag { get; set; }
    }
}
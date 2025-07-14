using System.ComponentModel.DataAnnotations;

namespace IAMNC0000S03.Business.Queries
{
    //查詢參數  
    public class BullientDataQueryDto
    {
        public string SystemCode { get; set; }
        public bool IsCurrentActive { get; set; }
        public string StartBullientDate { get; set; }
        public string OrderType { get; set; }
    }
}
namespace IAMNC0000S03.Business.Models
{
    public class BullientDataDto
    {
        public int MsgSeq { get; set; }
        public string SystemCode { get; set; }
        public string Subject { get; set; }
        public string EncodeContent { get; set; }
        public DateTime ValidStrartDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public string DisplayType { get; set; }
        public string IsTop { get; set; }
        public int TopOrder { get; set; }
        public DateTime TxtDate { get; set; }
        public string TxtUserId { get; set; }
        public string ProcessUrl { get; set; }
        public string UrlButtonName { get; set; }
        public string LimitReadMark { get; set; }
        public int CloseCountdown { get; set; }


        public List<AttachmentFile>? AttachmentFiles { get; set; }
    }

    public class AttachmentFile
    {
        public string ServiceType { get; set; }
        public string MsgSeq { get; set; }
        public string AttSeq { get; set; }
        public string FileName { get; set; }
    }
}
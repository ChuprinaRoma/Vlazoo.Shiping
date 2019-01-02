namespace Vazoo1123.Models
{
    public class Messages
    {
        public int ID { get; set; }
        public string eBaySiteCode { get; set; }
        public string ParentID { get; set; }
        public int AccountID { get; set; }
        public int Status { get; set; }
        public string ExternalID { get; set; }
        public string ExternalID2 { get; set; }
        public string EBayItemID { get; set; }
        public string SKU { get; set; }
        public string OrderID { get; set; }
        public string ExpirationDate { get; set; }
        public string RemoteFolder { get; set; }
        public int Importance { get; set; }
        public string MessageType { get; set; }
        public string QuestionType { get; set; }
        public string ReceiveDate
        {
            get
            {
                string dataTemp = DateEntered.Replace("Z", "");
                return dataTemp.Replace("T", "");
            }
            set
            {

            }
        }
        public string RecipientUserID { get; set; }
        public string ReadBy { get; set; }
        public string ReadAt { get; set; }
        public string ReplyID { get; set; }
        public string ReplyDate { get; set; }
        public string RepliedBy { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyShort { get; set; }
        public string Color
        {
            get => ParentID != null || Status == 2 ? "#b4fc9b" : "#eff299";
        }
        public string DateEntered { get; set; }
        public string DateCompleted { get; set; }
        public string DateArchived { get; set; }
        public bool IsFlagged { get; set; }
        public string FolderID { get; set; }
        public string LastNoted { get; set; }
        public string DateDeleted { get; set; }
        public string ImageFile { get; set; }
        public string MessageID { get; set; }
        public string ImageStatusMessage
        {
            get => $"https://vlazoo.com/images/{ImageFile}";
        }
    }
}

namespace Vazoo1123.Models
{
    public class OrderInfo
    {
        public int ID { get; set; }
        public string EBayUserID { get; set; }
        public string ShopperEmail { get; set; }
        public string EBayItemID { get; set; }
        public string ImageFile { get; set; }
        public string RecordNumber { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string ItemTitle { get; set; }
        public bool IsAutoPrint { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal TransactionPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string DateCreated { get; set; }
        public string WeightLBS { get; set; }
        public string WeightOZ { get; set; }
        public string WeightKG { get; set; }
        public string DimensionsL { get; set; }
        public string DimensionsH { get; set; }
        public string DimensionsW { get; set; }
        public string TrackingNumber { get; set; }
        public Carrier CarrierOptimal { get; set; }
        public CAddressBase ShipToAddress { get; set; }
    }
}
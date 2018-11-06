namespace Vazoo1123.ViewModels.Profile
{
    public class ProfileData
    {
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string ContactName { get; set; }
        public string WarehouseContactName { get; set; }
        public string ClientEmail { get; set; }
        public string LegalName { get; set; }
        public string ClientPhone { get; set; }
        public string WarehouseAddress1 { get; set; }
        public string WarehouseAddress2 { get; set; }
        public bool? UseSystemSMTP { get; set; }
        public string WarehouseFloor { get; set; }
        public string FromName { get; set; }
        public string WarehouseCity { get; set; }
        public string WarehouseState { get; set; }
        public string WarehouseZip { get; set; }
        public string FromEmail { get; set; }
        public string WarehousePhone { get; set; }
        public bool? ResidentialIndicator { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPLogin { get; set; }
        public string EBayUserID { get; set; }
        public string SMTPPassword { get; set; }
        public bool? SMTPSSL { get; set; }
        public string PayPalEmail { get; set; }
        public bool? OutOfStockControlPreference { get; set; }
        public string FeedbackMessage { get; set; }
        public bool? LunchCalculationExtended { get; set; }
        public string LunchTimeFree { get; set; }
        public string LunchTimeDefault { get; set; }
        public string Subscription { get; set; }
        public string RegistrationDate { get; set; }
        public string PostageBalance { get; set; }
        public string PrintManager { get; set; }
        public bool? AutoPrintLabels { get; set; }
        public bool? UseSystemUPS { get; set; }
        public string SignConfirmStartPriceUPS { get; set; }
        public bool? UseSystemUSPS { get; set; }
        public string SignConfirmStartPriceFedEx { get; set; }
        public bool? UseSystemFedEx { get; set; }
        public bool? PrintProductInfo { get; set; }
        public string AutoPrintLabelMaxPrice { get; set; }
        public string MaxShippingCostForAutoPrint { get; set; }
        public bool? MessageFilterEnabled { get; set; }
        public bool? MessageFilterNoDigits { get; set; }
        public string MessageFilterPositive { get; set; }
        public string MessageFilterNegative { get; set; }
        public string MessageFooter { get; set; }
        public bool? ProfileCompleted { get; set; }
    }
}
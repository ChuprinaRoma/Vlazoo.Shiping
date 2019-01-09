using System;

namespace Vazoo1123.Models
{
    public class Carrier
    {
        public double Id { get; set; }
        public int Company { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string EBayCode { get; set; }
        public double price = 0;
        public double Price
        {
            get
            {
                return Math.Round(price, 2);
                //if(price.ToString().IndexOf('.') != -1)
                //{
                //    string tmpPrice = price.ToString().Remove(0, price.ToString().IndexOf('.')+1);
                //    if(tmpPrice.Length > 2)
                //    {
                //        return price - price % 0.01;
                //    }
                //    else
                //    {
                //        return price;
                //    }
                //}
                //else
                //{
                //    return price;
                //}
            }
            set
            {
                price = value;
            }
        }
        public double Cost { get; set; }
        public string AccountID { get; set; }
        public bool IsL5 { get; set; }
        public string ShippoToken { get; set; }
        public bool IsShippo { get; set; }
        public string RateID { get; set; }
        public string CompanyName { get; set; }
    }
}
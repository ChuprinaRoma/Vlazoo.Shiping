﻿namespace Vazoo1123.Models
{
    public class Carrier
    {
        public double Id { get; set; }
        public int Company { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string EBayCode { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public string AccountID { get; set; }
        public bool IsL5 { get; set; }
        public string ShippoToken { get; set; }
        public bool IsShippo { get; set; }
        public string RateID { get; set; }
        public string CompanyName { get; set; }
    }
}
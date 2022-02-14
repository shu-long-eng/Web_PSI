using System;


namespace PSIsystem.Helpers
{
    public class PurchasesInfo
    {
        
        public string ID { get; set; }
        public int ProductType { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}
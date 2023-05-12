using System;

namespace goods_counting
{
    internal class Sell
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public string product { get; set; }
        public int count { get; set; }
        public decimal price { get; set; }
    }
}
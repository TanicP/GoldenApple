using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenAppleApp
{
    public class SubData
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid dataId { get; set; }

        public Data data { get; set; }
    }
}

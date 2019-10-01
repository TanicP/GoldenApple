using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenAppleApp
{
    public class Data
    {
        public Guid     Id { get; set; }
        public string   Name { get; set; }
        public int      Count { get; set; }
        public bool     Flag { get; set; }

        public Guid     PropId { get; set; }

        public Property Prop { get; set; }
        public ICollection<SubData> SubData { get; set; }
        
    }
}

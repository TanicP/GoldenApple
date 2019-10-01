using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenAppleApp
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public ICollection<Data> Data { get; set; }
    }
    
}


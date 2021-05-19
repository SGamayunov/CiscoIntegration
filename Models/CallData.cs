using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiscoIntegration.Models
{
    public class CallData
    {
        public string Address { get; set; }
        public string Agent { get; set; }
        public string Ext { get; set; }
        public string ANI { get; set; }
        public string Lang { get; set; }
    }
}

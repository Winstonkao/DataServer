using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServerApi
{
    public class Company
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public DateTime updateTime { get; set; }
    }
}

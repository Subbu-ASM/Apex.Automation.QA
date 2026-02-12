using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Models
{
        public class TestStep
        {
            public string Action { get; set; }
            public string Target { get; set; }
            public string Expected { get; set; }
            public string Value { get; set; }
            public int TimeoutMs { get; set; }
        }
}

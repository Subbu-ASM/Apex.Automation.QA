using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Data.Models
{
    public class TestStepModel
    {
        public string Action { get; set; }
        public string Target { get; set; }

        public string Value { get; set; }        // optional
        public string ValueKey { get; set; }     // optional (DB)
        public string Expected { get; set; }     // validation
        public int? TimeoutMs { get; set; }      // waits
    }
}

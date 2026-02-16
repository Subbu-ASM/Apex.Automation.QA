using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Engine
{
    public class ExecutionResult
    {
        public string TestCaseId { get; set; }
        public string TestName { get; set; }

        public bool IsPassed { get; set; }
        public string FailureStep { get; set; }
        public string Message { get; set; }
    }
}

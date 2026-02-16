using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Data.Models
{
    public class TestCaseModel
    {
        public string TestCaseId { get; set; }
        public string TestName { get; set; }
        public string Page { get; set; }

        public List<TestStepModel> Steps { get; set; }
    }
}

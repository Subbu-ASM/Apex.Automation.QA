using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Models
{
    public class TestCaseModel
    {
        public string TestName { get; set; }
        public List<TestStep> Steps { get; set; }
    }
}
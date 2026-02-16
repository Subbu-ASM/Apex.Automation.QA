using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Core;

namespace Automation.Test.Common.BaseTests
{
    public class CommonFlows
    {
        protected StepEngine _engine;
        protected string _testCaseRoot;

        public void RunLoginFlow()
        {
            var testCasePath = Path.Combine(_testCaseRoot, "LoginFlow.json");
            var testCase = TestCaseLoader.Load(testCasePath);

            foreach (var step in testCase.Steps)
            {
                _engine.Execute(step);
            }
        }

        // Later you can add:
        // public void RunMenuNavigationFlow() { }
        // public void RunHealthCheckFlow() { }
    }
}


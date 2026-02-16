using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation.Framework.Data.Models;
using System.Threading.Tasks;

namespace Automation.Framework.Data.Db
{
    public class DbService : IDbService
    {
        private readonly Dictionary<string, string> _testData =
            new Dictionary<string, string>
            {
                { "AdminPassword", "12345" },
                { "OperatorPassword", "11111" }
            };

        public string GetTestData(string key)
        {
            if (_testData.ContainsKey(key))
                return _testData[key];

            throw new Exception($"Test data not found for key: {key}");
        }

        public void SaveTestResult(TestResultEntity result)
        {
            // TEMP: just simulate DB save
            Console.WriteLine($"[DB] Saved Result: {result.TestCaseId} - {result.IsPassed}");
        }
    }
}

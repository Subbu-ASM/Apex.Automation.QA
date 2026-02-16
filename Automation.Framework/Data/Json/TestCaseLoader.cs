using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Automation.Framework.Data.Models;

namespace Automation.Framework.Data.Json
{
    public static class TestCaseLoader
    {
        public static TestCaseModel Load(string testCasePath)
        {
            if (!File.Exists(testCasePath))
            {
                throw new FileNotFoundException(
                    $"TestCase JSON not found at path: {testCasePath}");
            }

            string json = File.ReadAllText(testCasePath);

            var testCase = JsonSerializer.Deserialize<TestCaseModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (testCase == null)
            {
                throw new InvalidDataException("Failed to deserialize TestCase JSON");
            }

            return testCase;
        }
    }
}

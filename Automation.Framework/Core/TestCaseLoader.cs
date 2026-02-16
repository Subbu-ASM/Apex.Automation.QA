using Automation.Framework.Models;
using System.Text.Json;

namespace Automation.Framework.Core
{
    public static class TestCaseLoader
    {
        public static TestCaseModel Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Test case not found: {path}");

            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<TestCaseModel>(json)
                   ?? throw new InvalidOperationException("Invalid test case JSON");
        }
    }
}

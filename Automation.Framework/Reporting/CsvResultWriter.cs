using System;
using System.IO;
using Automation.Framework.Engine;

namespace Automation.Framework.Reporting
{
    public static class CsvResultWriter
    {
        private static readonly object _lock = new();

        public static void WriteHeaderIfNotExists(string csvPath)
        {
            if (!File.Exists(csvPath))
            {
                File.WriteAllText(csvPath,
                    "RunId,MachineName,TestSuite,TestCaseId,TestCaseName,Expected,Actual,Status\n");
            }
        }

        public static void AppendResult(
            string csvPath,
            string runId,
            string machineName,
            string testSuite,
            ExecutionResult result)
        {
            lock (_lock)
            {
                WriteHeaderIfNotExists(csvPath);

                var line =
                    $"{runId}," +
                    $"{machineName}," +
                    $"{testSuite}," +
                    $"{result.TestCaseId}," +
                    $"{result.TestName}," +
                    $"\"{result.Expected}\"," +
                    $"\"{result.Actual}\"," +
                    $"{(result.IsPassed ? "PASS" : "FAIL")}\n";

                File.AppendAllText(csvPath, line);
            }
        }
    }
}
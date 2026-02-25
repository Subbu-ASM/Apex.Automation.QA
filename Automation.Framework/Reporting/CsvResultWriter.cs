using Automation.Framework.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Automation.Framework.Reporting
{
    public static class CsvResultWriter
    {
        public static void Write(List<ExecutionResult> results, string resultRoot, string runId)
        {
            Directory.CreateDirectory(resultRoot);

            var csvPath = Path.Combine(resultRoot, $"TestResults_{runId}.csv");

            var sb = new StringBuilder();
            sb.AppendLine("RunTime,TestCaseId,TestCaseName,Status,FailureStep,Message");

            foreach (var r in results)
            {
                sb.AppendLine(
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}," +
                    $"{Escape(r.TestCaseId)}," +
                    $"{Escape(r.TestName)}," +
                    $"{(r.IsPassed ? "PASS" : "FAIL")}," +
                    $"{Escape(r.FailureStep)}," +
                    $"{Escape(r.Message)}"
                );
            }

            File.WriteAllText(csvPath, sb.ToString());
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
    }
}
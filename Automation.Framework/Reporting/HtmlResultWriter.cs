//using Automation.Framework.Engine;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace Automation.Framework.Reporting
//{
//    public static class HtmlResultWriter
//    {
//        public static void Write(List<ExecutionResult> results, string folderPath)
//        {
//            Directory.CreateDirectory(folderPath);

//            var filePath = Path.Combine(
//                folderPath,
//                $"TestResults_{DateTime.Now:yyyyMMdd_HHmmss}.html");

//            var sb = new StringBuilder();

//            sb.AppendLine("<html><head>");
//            sb.AppendLine("<style>");
//            sb.AppendLine("body { font-family: Segoe UI; }");
//            sb.AppendLine("table { border-collapse: collapse; width: 100%; }");
//            sb.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; }");
//            sb.AppendLine(".pass { background-color: #d4edda; }");
//            sb.AppendLine(".fail { background-color: #f8d7da; }");
//            sb.AppendLine("</style>");
//            sb.AppendLine("</head><body>");
//            sb.AppendLine("<h2>Automation Test Results</h2>");
//            sb.AppendLine($"<p>Executed At: {DateTime.Now}</p>");

//            sb.AppendLine("<table>");
//            sb.AppendLine("<tr><th>TestCaseId</th><th>TestName</th><th>Status</th><th>FailureStep</th><th>Message</th></tr>");

//            foreach (var r in results)
//            {
//                var css = r.IsPassed ? "pass" : "fail";

//                sb.AppendLine($"<tr class='{css}'>");
//                sb.AppendLine($"<td>{r.TestCaseId}</td>");
//                sb.AppendLine($"<td>{r.TestName}</td>");
//                sb.AppendLine($"<td>{(r.IsPassed ? "PASS" : "FAIL")}</td>");
//                sb.AppendLine($"<td>{r.FailureStep}</td>");
//                sb.AppendLine($"<td>{r.Message}</td>");
//                sb.AppendLine("</tr>");
//            }

//            sb.AppendLine("</table>");
//            sb.AppendLine("</body></html>");

//            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
//        }
//    }
//}
using Automation.Framework.Engine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Automation.Framework.Reporting
{
    public static class HtmlResultWriter
    {
        public static void Write(List<ExecutionResult> results, string resultRoot, string runId)
        {
            Directory.CreateDirectory(resultRoot);

            var htmlPath = Path.Combine(resultRoot, $"TestResults_{runId}.html");

            var sb = new StringBuilder();
            sb.AppendLine("<html><head><title>Automation Test Report</title></head><body>");
            sb.AppendLine("<h2>Automation Test Execution Report</h2>");
            sb.AppendLine("<table border='1' cellspacing='0' cellpadding='5'>");
            sb.AppendLine("<tr><th>TestCaseId</th><th>TestCaseName</th><th>Status</th><th>FailureStep</th><th>Message</th></tr>");

            foreach (var r in results)
            {
                var color = r.IsPassed ? "green" : "red";

                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{r.TestCaseId}</td>");
                sb.AppendLine($"<td>{r.TestName}</td>");
                sb.AppendLine($"<td style='color:{color};font-weight:bold'>{(r.IsPassed ? "PASS" : "FAIL")}</td>");
                sb.AppendLine($"<td>{r.FailureStep}</td>");
                sb.AppendLine($"<td>{r.Message}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            File.WriteAllText(htmlPath, sb.ToString());
        }
    }
}
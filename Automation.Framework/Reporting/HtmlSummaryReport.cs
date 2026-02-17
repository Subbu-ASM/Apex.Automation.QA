using System.IO;
using System.Text;

namespace Automation.Framework.Reporting
{
    public static class HtmlSummaryReport
    {
        public static void Generate(
            string htmlPath,
            int total,
            int passed,
            int failed)
        {
            var html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>Automation Summary Report</title>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Arial; background: #f4f6f8; }");
            html.AppendLine(".card { width: 400px; margin: 100px auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); }");
            html.AppendLine(".pass { color: green; font-weight: bold; }");
            html.AppendLine(".fail { color: red; font-weight: bold; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='card'>");
            html.AppendLine("<h1>Test Execution Summary</h1>");
            html.AppendLine($"<p>Total Tests: <b>{total}</b></p>");
            html.AppendLine($"<p class='pass'>Passed: {passed}</p>");
            html.AppendLine($"<p class='fail'>Failed: {failed}</p>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(htmlPath, html.ToString());
        }
    }
}
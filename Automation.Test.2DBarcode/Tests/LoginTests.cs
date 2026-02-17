using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Automation.Framework.Data.Db;
using Automation.Framework.Data.Json;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;
using Automation.Framework.UI.Driver;
using Automation.Framework.Reporting;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Test._2DBarcode.Tests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_Admin_Flow()
        {
            // 1️⃣ Load Test Case JSON
            var testCase = TestCaseLoader.Load(
                @"TestCases\LoginFlow_Admin.json");

            // 2️⃣ Start Application (EXE path, not folder)
            var exePath = @"C:\NewClone\apex\UI\bin\Debug\net8.0-windows8.0\UI.exe";
            var process = Process.Start(exePath);
            Thread.Sleep(30000);

            Assert.IsNotNull(process);

            using var app = Application.Attach(process);
            using var automation = new UIA3Automation();

            // 3️⃣ Get Main Window
            var mainWindow = app.GetMainWindow(automation);
            Assert.IsNotNull(mainWindow);

            // 4️⃣ Load UiMap.json
            var uiMapJson = File.ReadAllText(@"UiMaps\UiMap.json");

            UiMapModel uiMap = JsonSerializer.Deserialize<UiMapModel>(uiMapJson)
                ?? throw new InvalidOperationException("UiMap.json invalid");

            // 5️⃣ Create UI Driver
            var uiDriver = new FlaUiDriver(mainWindow, uiMap);

            // 6️⃣ Create DB Service
            var dbService = new DbService();

            // 7️⃣ Create Action Context
            var context = new ActionContext
            {
                UiDriver = uiDriver,
                DbService = dbService,
                UiMap = uiMap,
                CurrentPage = "LoginPage"
            };

            // 8 Execute Test
            var engine = new TestEngine(context);
            var result = engine.Execute(testCase);

            //  Run details
            string runId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string machineName = Environment.MachineName;
            string testSuite = "Login";

            //  Results folder
            Directory.CreateDirectory("Results");

            string csvPath = Path.Combine("Results", "TestResults.csv");

            //  Write CSV
            CsvResultWriter.AppendResult(
                csvPath,
                runId,
                machineName,
                testSuite,
                result);

            //  Maintain counters (static for demo, later can be global)
            int total = 1;
            int passed = result.IsPassed ? 1 : 0;
            int failed = result.IsPassed ? 0 : 1;

            //  Generate HTML summary
            string htmlPath = Path.Combine("Results", "Summary.html");

            HtmlSummaryReport.Generate(
                htmlPath,
                total,
                passed,
                failed);

            //  Auto open summary
            Process.Start(new ProcessStartInfo
            {
                FileName = htmlPath,
                UseShellExecute = true
            });

            //  Final assert
            Assert.IsTrue(result.IsPassed, result.Message);
        }
    }
}
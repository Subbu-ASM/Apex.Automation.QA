using Automation.Framework.Core;
using Automation.Framework.Data.Json;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;
using Automation.Framework.Reporting;
using Automation.Framework.UI.Driver;
using Automation.Framework.Utilities;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Automation.Test._2DBarcode.Tests
{
    [TestClass]
    public class LoginTests
    {
        private Application _app;
        private UIA3Automation _automation;
        private TestEngine _engine;
        private string _commonTestCaseRoot;
        private string _resultRoot;
        private string _runId;
        

        [TestInitialize]
        public void Setup()
        {
            // 1️⃣ Load machine config
            var machineConfig = MachineConfigLoader.Load("MachineConfig.json");

            // 2️⃣ Get project root (Automation.Test.2DBarcode)
            var projectRoot = Directory
                .GetParent(AppContext.BaseDirectory)!   // bin\Debug\net8.0-windows
                .Parent!                                // Debug
                .Parent!                                // bin
                .Parent!                                // Automation.Test.2DBarcode
                .FullName;

            // 3️⃣ Get solution root (H:\NewAuto)
            var solutionRoot = Directory.GetParent(projectRoot)!.FullName;

            // 4️⃣ Build correct paths

            var commonUiMapPath = Path.Combine(
                solutionRoot,
                "Automation.Test.Common",
                "UiMaps",
                "UiMap.json"
            );

            var machineUiMapPath = Path.Combine(
                projectRoot,
                "UiMaps",
                "UiMap.json"
            );

            _commonTestCaseRoot = Path.Combine(
                solutionRoot,
                "Automation.Test.Common",
                "TestCases"
            );

            // 5️⃣ Create machine-specific result folder
            _runId = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            _resultRoot = Path.Combine(
                projectRoot,
                machineConfig.ResultRoot,
                _runId
            );

            Directory.CreateDirectory(_resultRoot);

            _commonTestCaseRoot = Path.Combine(solutionRoot, machineConfig.CommonTestCaseRoot);

            // 3️⃣ Launch application
            _app = Application.Launch(machineConfig.AppPath);
            _automation = new UIA3Automation();

            // 4️⃣ Wait for real MainWindow (not splash screen / error popup)
            var mainWindow = WindowWaiter.WaitForMainWindow(_app, _automation, "MainWindowRoot");

            // 5️⃣ Wait until Login UI is visible (stable element)
            WaitHelper.Until(
                () =>
                {
                    try
                    {
                        return mainWindow.FindFirstDescendant(cf =>
                            cf.ByAutomationId("btnLoginEnable")) != null
                               ||
                               mainWindow.FindFirstDescendant(cf =>
                            cf.ByName("Login")) != null;
                    }
                    catch
                    {
                        return false;
                    }
                },
                TimeSpan.FromSeconds(30)
            );

            //// 6️⃣ Load Common UiMap (Login UI)
            var commonUiMapJson = File.ReadAllText(commonUiMapPath);
            var commonUiMap = JsonSerializer.Deserialize<UiMapModel>(commonUiMapJson)
                              ?? throw new InvalidOperationException("Common UiMap.json invalid");

            // 7️⃣ Load Machine UiMap (optional)
            UiMapModel machineUiMap;
            if (File.Exists(machineUiMapPath))
            {
                var machineUiMapJson = File.ReadAllText(machineUiMapPath);
                machineUiMap = JsonSerializer.Deserialize<UiMapModel>(machineUiMapJson)
                               ?? new UiMapModel();
            }
            else
            {
                machineUiMap = new UiMapModel();
            }

            // 8️⃣ Merge UI maps (machine overrides common)
            var mergedUiMap = new UiMapModel();

            foreach (var page in commonUiMap.Pages)
                mergedUiMap.Pages[page.Key] = new Dictionary<string, string>(page.Value);

            foreach (var page in machineUiMap.Pages)
            {
                if (!mergedUiMap.Pages.ContainsKey(page.Key))
                    mergedUiMap.Pages[page.Key] = new Dictionary<string, string>();

                foreach (var control in page.Value)
                    mergedUiMap.Pages[page.Key][control.Key] = control.Value;
            }

            // 9️⃣ Init UI driver
            var uiDriver = new FlaUiDriver(mainWindow, mergedUiMap);

            // 🔟 Init engine context
            var actionContext = new ActionContext
            {
                UiDriver = uiDriver,
                UiMap = mergedUiMap,
                CurrentPage = "LoginPage",
                DbService = null,
                ResultRoot = _resultRoot
            };

            _engine = new TestEngine(actionContext);

        }


        [TestMethod]
        public void Login_All_Tests()
        {
            var testFiles = Directory.GetFiles(_commonTestCaseRoot, "*.json");

            var allResults = new List<ExecutionResult>();

            foreach (var file in testFiles)
            {
                var testCase = TestCaseLoader.Load(file);
                var result = _engine.Execute(testCase);
                allResults.Add(result);
            }

            // ✅ Correct usage
            CsvResultWriter.Write(allResults, _resultRoot,_runId);
            HtmlResultWriter.Write(allResults, _resultRoot,_runId);
        }


        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                _automation?.Dispose();
                _app?.Close();
            }
            catch
            {
                // Ignore cleanup issues
            }
        }
    }
}
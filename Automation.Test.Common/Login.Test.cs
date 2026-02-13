using Automation.Framework.Core; 
using Automation.Framework.Logging;
using Automation.Framework.Models;
using Automation.Framework.UI;   
using FlaUI.Core;               
using FlaUI.UIA3;               
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Automation.Test.Common
{
        public class SmokeTests
        {
            private Application _app;
            private UIA3Automation _automation;
            private StepEngine _engine;

            [TestInitialize]
        public void Setup()
        {
            // 1️⃣ Load machine config (per project)
            var machineConfig = MachineConfigLoader.Load("MachineConfig.json");

            // 2️⃣ Launch machine-specific WPF app
            _app = Application.Launch(machineConfig.AppPath);
            _automation = new UIA3Automation();
            var mainWindow = _app.GetMainWindow(_automation);

            // 3️⃣ Load UiMap.json
            var uiMapJson = File.ReadAllText(machineConfig.UiMapPath);
            var uiMap = JsonSerializer.Deserialize<Dictionary<string, string>>(uiMapJson)
                        ?? throw new InvalidOperationException("UiMap.json invalid");

            // 4️⃣ Init UI driver + engine
            var uiDriver = new FlaUiDriver(mainWindow, uiMap, _automation);
            _engine = new StepEngine(uiDriver);

            // 5️⃣ Init log watcher (if you use it)
            _logWatcher = new AppLogWatcher(machineConfig.LogPath);

            // 6️⃣ Prepare TestCase root + result folder
            _testCaseRoot = machineConfig.TestCaseRoot;

            _runFolder = Path.Combine(machineConfig.ResultRoot,
                $"{machineConfig.MachineName}_{DateTime.Now:yyyyMMdd_HHmmss}");

            Directory.CreateDirectory(_runFolder);
            Directory.CreateDirectory(Path.Combine(_runFolder, "screenshots"));
        }

        [TestMethod]
    
        public void Login_From_Json_Test()
        {
            var testCasePath = Path.Combine(_testCaseRoot, "LoginFlow.json");
            var testCase = TestCaseLoader.Load(testCasePath);

            int stepIndex = 0;

            foreach (var step in testCase.Steps)
            {
                try
                {
                    _engine.Execute(step);
                    stepIndex++;
                }
                catch
                {
                    var screenshotPath = Path.Combine(_runFolder, "screenshots",
                        $"LoginFlow_Step{stepIndex}_{DateTime.Now:HHmmss}.png");

                    _uiDriver.CaptureScreenshot(screenshotPath);
                    throw;
                }
            }
        }

        [TestCleanup]
                public void Cleanup()
                {
                    _automation.Dispose();
                    _app.Close();
                }
        }
    
}

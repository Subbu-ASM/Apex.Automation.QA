using Automation.Framework.Core;
using Automation.Framework.Logging;
using Automation.Framework.Data.Models;
using Automation.Framework.UI;
using FlaUI.Core;
using FlaUI.UIA3;
using Automation.Framework.Data.Json;
using System.Text.Json;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Automation.Framework.Engine;
using System.Collections.Generic;
using Automation.Framework.UI.Driver;

namespace Automation.Test.Common
{
    public class SmokeTests
    {
        private string _testCaseRoot = string.Empty;
        private string _runFolder = string.Empty;
        private Application _app;
        private UIA3Automation _automation;
        private TestEngine _engine;
        

        [TestInitialize]
        public void Setup()
        {
            var machineConfig = MachineConfigLoader.Load("MachineConfig.json");

            _app = Application.Launch(machineConfig.AppPath);
            _automation = new UIA3Automation();
            var mainWindow = _app.GetMainWindow(_automation);

            var uiMapJson = File.ReadAllText(machineConfig.UiMapPath);
            var uiMap = JsonSerializer.Deserialize<UiMapModel>(uiMapJson)
                        ?? throw new InvalidOperationException("UiMap.json invalid");

            var uiDriver = new FlaUiDriver(mainWindow, uiMap);

            var actionContext = new ActionContext
            {
                UiDriver = uiDriver,
                UiMap = uiMap,
                CurrentPage = "LoginPage"
            };

            // 🔥 MISSING LINE – NOW FIXED
            _engine = new TestEngine(actionContext);

            _testCaseRoot = machineConfig.TestCaseRoot;

            _runFolder = Path.Combine(
                machineConfig.ResultRoot,
                $"{machineConfig.MachineName}_{DateTime.Now:yyyyMMdd_HHmmss}");

            Directory.CreateDirectory(_runFolder);
            Directory.CreateDirectory(Path.Combine(_runFolder, "screenshots"));
        }

        [TestMethod]

        public void Login_From_Json_Test()
        {
            var testCasePath = Path.Combine(_testCaseRoot, "LoginFlow.json");
            var testCase = TestCaseLoader.Load(testCasePath);

            _engine.Execute(testCase);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _automation.Dispose();
            _app.Close();
        }
    }

}

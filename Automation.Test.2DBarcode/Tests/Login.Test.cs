using Automation.Framework.Core;
using Automation.Framework.Data.Json;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;
using Automation.Framework.UI.Driver;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Automation.Framework.Utilities;

namespace Automation.Test._2DBarcode.Tests
{
    [TestClass]
    public class LoginTests
    {
        private Application _app;
        private UIA3Automation _automation;
        private TestEngine _engine;

        [TestInitialize]
        public void Setup()
        {
            // 1️⃣ Load machine config
            var machineConfig = MachineConfigLoader.Load("MachineConfig.json");

            // 2️⃣ Launch application
            _app = Application.Launch(machineConfig.AppPath);
            _automation = new UIA3Automation();
            var mainWindow = WindowWaiter.WaitForMainWindow(_app, _automation, "MainWindowRoot");


            //var mainWindow = _app.GetMainWindow(_automation)
            //                ?? throw new InvalidOperationException("Main window not found");

            // ✅ Wait until login UI is visible (use any stable element on login page)
            WaitHelper.Until(
                () =>
                {
                    try
                    {
                        return mainWindow.FindFirstDescendant(cf =>
                            cf.ByAutomationId("btnLoginEnable")) != null;
                    }
                    catch
                    {
                        return false;
                    }
                },
                TimeSpan.FromSeconds(20)
            );


            // 3️⃣ Load Common UiMap (Login UI)
            var commonUiMapJson = File.ReadAllText(machineConfig.CommonUiMapPath);
            var commonUiMap = JsonSerializer.Deserialize<UiMapModel>(commonUiMapJson)
                              ?? throw new InvalidOperationException("Common UiMap.json invalid");

            // 4️⃣ Load Machine UiMap (Machine-specific screens, optional)
            UiMapModel machineUiMap;
            if (File.Exists(machineConfig.UiMapPath))
            {
                var machineUiMapJson = File.ReadAllText(machineConfig.UiMapPath);
                machineUiMap = JsonSerializer.Deserialize<UiMapModel>(machineUiMapJson)
                               ?? new UiMapModel();
            }
            else
            {
                machineUiMap = new UiMapModel();
            }

            // 5️⃣ Merge UI maps (machine overrides common per page/control)
            var mergedUiMap = new UiMapModel();

            // Copy common pages
            foreach (var page in commonUiMap.Pages)
            {
                mergedUiMap.Pages[page.Key] = new Dictionary<string, string>(page.Value);
            }

            // Merge machine pages
            foreach (var page in machineUiMap.Pages)
            {
                if (!mergedUiMap.Pages.ContainsKey(page.Key))
                {
                    mergedUiMap.Pages[page.Key] = new Dictionary<string, string>();
                }

                foreach (var control in page.Value)
                {
                    mergedUiMap.Pages[page.Key][control.Key] = control.Value;
                }
            }

            // 6️⃣ Init UI driver
            var uiDriver = new FlaUiDriver(mainWindow, mergedUiMap);

            // 7️⃣ Init engine context
            var actionContext = new ActionContext
            {
                UiDriver = uiDriver,
                UiMap = mergedUiMap,
                CurrentPage = "LoginPage",
                DbService = null
            };

            _engine = new TestEngine(actionContext);
        }
        
        [TestMethod]
        public void Login_Admin_Flow()
        {
            // 8️⃣ Load Login test case from Common (config-driven)
            var machineConfig = MachineConfigLoader.Load("MachineConfig.json");
            var loginTestCasePath = Path.Combine(machineConfig.CommonTestCaseRoot, "LoginFlow.json");

            var testCase = TestCaseLoader.Load(loginTestCasePath);

            var result = _engine.Execute(testCase);

            if (!result.IsPassed)
            {
                Assert.Fail($"Login failed at step '{result.FailureStep}'. Error: {result.Message}");
            }
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
                // ignore cleanup issues
            }
        }
    }
}

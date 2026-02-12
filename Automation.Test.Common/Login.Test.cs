using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;               
using FlaUI.UIA3;               
using Automation.Framework.Core; 
using Automation.Framework.UI;   
using Automation.Framework.Models;

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
                _app = Application.Launch(@"C:\Path\To\Your\WpfApp.exe");
                _automation = new UIA3Automation();

                var mainWindow = _app.GetMainWindow(_automation);

                var uiMap = new Dictionary<string, string>
            {
                 { "OpenLoginButton", "BTN_OPEN_LOGIN" },
                 { "AccessTypeCombo", "CMB_ACCESS_TYPE" },
                 { "PasswordTextBox", "TXT_PASSWORD" },
                 { "ConfirmLoginButton", "BTN_CONFIRM_LOGIN" },
                 { "LoginStatusLabel", "LBL_LOGIN_STATUS" }
            };

                var uiDriver = new FlaUiDriver(mainWindow, uiMap);
                _engine = new StepEngine(uiDriver);
            }

            [TestMethod]
            public void Click_Start_Button()
            {
                _engine.Execute(new Automation.Framework.Models.TestStep
                {
                    Action = "Click",
                    Target = "StartButton"
                });
            }

            [TestCleanup]
            public void Cleanup()
            {
                _automation.Dispose();
                _app.Close();
            }
        }
    
}

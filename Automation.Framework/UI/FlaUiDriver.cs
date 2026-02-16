using Automation.Framework.Logging;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.UI
{
    public class FlaUiDriver : IUiDriver
    {
        private readonly AutomationElement _mainWindow;
        private readonly IDictionary<string, string> _uiMap;
        private readonly UIA3Automation _automation;

        public FlaUiDriver(AutomationElement mainWindow, IDictionary<string, string> uiMap, UIA3Automation automation)
        {
            _mainWindow = mainWindow;
            _uiMap = uiMap;
            _automation = automation;
        }


        private AutomationElement Find(string logicalName)
        {
            if (!_uiMap.TryGetValue(logicalName, out var automationId))
                throw new KeyNotFoundException($"UI Map does not contain key '{logicalName}'");

            var element = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(automationId));

            if (element == null)
                throw new InvalidOperationException($"UI element '{logicalName}' not found (AutomationId: {automationId})");

            return element;
        }

        public void Click(string logicalName)
        {
            Find(logicalName)?.AsButton()?.Invoke();
        }

        public string ReadText(string logicalName)
        {
            return Find(logicalName)?.AsLabel()?.Text ?? string.Empty;
        }

        public bool IsEnabled(string logicalName)
        {
            return Find(logicalName)?.IsEnabled ?? false;
        }
        public void SetText(string logicalName, string text)
        {
            var element = Find(logicalName);
            element.AsTextBox()?.Enter(text);
        }
        public void SelectCombo(string logicalName, string itemText)
        {
            var combo = Find(logicalName).AsComboBox();
            combo.Expand();
            var item = combo.Items.FirstOrDefault(i => i.Text == itemText);
            item?.Select();
        }
        public void CaptureScreenshot(string filePath)
        {
            var screenshot = Capture.MainScreen();
            screenshot.ToFile(filePath);
        }
        public bool IsVisible(string logicalName)
        {
            var element = Find(logicalName);
            // An element is considered visible if it is not offscreen
            return element != null && !element.IsOffscreen;
        }
    }
}
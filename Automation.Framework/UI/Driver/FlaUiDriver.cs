using System;
using FlaUI.Core.AutomationElements;
using Automation.Framework.Data.Models;

namespace Automation.Framework.UI.Driver
{
    public class FlaUiDriver : IUiDriver
    {
        private readonly Window _mainWindow;
        private readonly UiMapModel _uiMap;

        public FlaUiDriver(Window mainWindow, UiMapModel uiMap)
        {
            _mainWindow = mainWindow
                ?? throw new ArgumentNullException(nameof(mainWindow));

            _uiMap = uiMap
                ?? throw new ArgumentNullException(nameof(uiMap));
        }

        // 🔹 CORE RESOLVER (MOST IMPORTANT METHOD)
        private AutomationElement Find(string page, string logicalName)
        {
            var automationId = _uiMap.GetAutomationId(page, logicalName);

            var element = _mainWindow.FindFirstDescendant(cf =>
                cf.ByAutomationId(automationId));

            if (element == null)
                throw new Exception(
                    $"Element not found | Page: {page}, Target: {logicalName}, AutomationId: {automationId}");

            return element;
        }

        // 🔹 ACTIONS

        public void Click(string page, string logicalName)
        {
            Find(page, logicalName)
                .AsButton()
                ?.Invoke();
        }

        public void SetText(string page, string logicalName, string value)
        {
            var textBox = Find(page, logicalName).AsTextBox();
            textBox.Text = string.Empty;
            textBox.Enter(value);
        }

        public void SelectCombo(string page, string logicalName, string value)
        {
            var combo = Find(page, logicalName).AsComboBox();
            combo.Select(value);
        }

        public string GetText(string page, string logicalName)
        {
            var element = Find(page, logicalName);
            return element.Name ?? element.AsLabel()?.Text ?? string.Empty;
        }

        public bool IsVisible(string page, string logicalName)
        {
            var element = Find(page, logicalName);
            return element != null && !element.BoundingRectangle.IsEmpty;
        }
    }
}
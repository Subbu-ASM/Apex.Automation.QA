using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using Automation.Framework.Data.Models;
using System;

namespace Automation.Framework.UI.Driver
{
    public class FlaUiDriver : IUiDriver
    {
        private readonly AutomationElement _mainWindow;
        private readonly UiMapModel _uiMap;

        public FlaUiDriver(AutomationElement mainWindow, UiMapModel uiMap)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _uiMap = uiMap ?? throw new ArgumentNullException(nameof(uiMap));
        }


        // ✅ ROOT RESOLVER (MainWindowRoot)
        private AutomationElement GetRoot()
        {
            if (!string.IsNullOrWhiteSpace(_uiMap.Root))
            {
                var root = _mainWindow.FindFirstDescendant(cf =>
                    cf.ByAutomationId(_uiMap.Root));

                if (root == null)
                    throw new InvalidOperationException($"Root '{_uiMap.Root}' not found");

                return root;
            }

            return _mainWindow;
        }

        // ✅ CORE FIND METHOD (Always uses root)
        private AutomationElement Find(string page, string logicalName)
        {
            var automationId = _uiMap.GetAutomationId(page, logicalName);

            var element = GetRoot().FindFirstDescendant(cf =>
                cf.ByAutomationId(automationId));

            if (element == null)
                throw new Exception(
                    $"Element not found | Page: {page}, Target: {logicalName}, AutomationId: {automationId}");

            return element;
        }

        // ✅ ACTIONS

        public void Click(string page, string logicalName)
        {
            var element = Find(page, logicalName).AsButton();
            element.Invoke();
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
            return Find(page, logicalName).AsTextBox().Text;
        }

        public bool IsVisible(string page, string logicalName)
        {
            try
            {
                var automationId = _uiMap.GetAutomationId(page, logicalName);
                var element = GetRoot().FindFirstDescendant(cf =>
                    cf.ByAutomationId(automationId));

                return !element.IsOffscreen && element.IsEnabled;
            }
            catch
            {
                return false;
            }
        }
    }
}

using Automation.Framework.Core;
using Automation.Framework.Data.Models;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
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

            if (combo == null)
                throw new InvalidOperationException($"Target '{logicalName}' is not a ComboBox");
            combo.Expand();
            //Thread.Sleep(500); // Wait for items to load (consider better wait strategy)
            WaitHelper.Until(() => combo.Items.Length > 0, TimeSpan.FromSeconds(3));

            // Select by index
            if (int.TryParse(value, out int index))
            {
                if (index < 0 || index >= combo.Items.Length)
                    throw new IndexOutOfRangeException(
                        $"Combo index {index} out of range (0 - {combo.Items.Length - 1})");

                combo.Select(index);
                return;
            }

            // Select by visible text
            var item = combo.Items
                .FirstOrDefault(i => i.Text.Equals(value, StringComparison.OrdinalIgnoreCase));

            if (item == null)
                throw new InvalidOperationException(
                    $"Combo item '{value}' not found in '{logicalName}'");

            item.Select();
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

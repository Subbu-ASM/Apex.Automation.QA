using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace Automation.Framework.UI
{
    public class FlaUiDriver : IUiDriver
    {
        private readonly AutomationElement _mainWindow;
        private readonly IDictionary<string, string> _uiMap;

        public FlaUiDriver(AutomationElement mainWindow, IDictionary<string, string> uiMap)
        {
            _mainWindow = mainWindow;
            _uiMap = uiMap;
        }

        private AutomationElement Find(string logicalName)
        {
            var automationId = _uiMap[logicalName];
            return _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(automationId));
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

    }
}
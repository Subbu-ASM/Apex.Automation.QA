using System;

namespace Automation.Framework.UI.Driver
{
    public interface IUiDriver
    {
        void Click(string page, string logicalName);

        void SetText(string page, string logicalName, string value);

        void SelectCombo(string page, string logicalName, string value);

        string GetText(string page, string logicalName);

        bool IsVisible(string page, string logicalName);
    }
}
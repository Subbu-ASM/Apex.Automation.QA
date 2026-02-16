using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Actions;
using Automation.Framework.Actions;


namespace Automation.Framework.Engine
{
    public static class ActionDispatcher
    {
        public static IAction GetAction(string actionName)
        {
            return actionName switch
            {
                "Click" => new ClickAction(),
                "SetText" => new SetTextAction(),
                "SelectCombo" => new SelectComboAction(),
                "WaitForVisible" => new WaitForVisibleAction(),
                "ValidateText" => new ValidateTextAction(),

                _ => throw new NotSupportedException(
                        $"Action '{actionName}' is not supported")
            };
        }
    }
}

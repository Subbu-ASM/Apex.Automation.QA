using System;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;

namespace Automation.Framework.Actions
{
    public class SelectComboAction : IAction
    {
        public void Execute(ActionContext context, TestStepModel step)
        {
            if (context.UiDriver == null)
                throw new InvalidOperationException("UiDriver is not initialized");

            if (string.IsNullOrWhiteSpace(step.Value))
                throw new ArgumentException("SelectCombo requires 'Value' in JSON");

            // Delegate to driver (no FlaUI here)
            context.UiDriver.SelectCombo(
                context.CurrentPage,
                step.Target,
                step.Value
            );
        }
    }
}
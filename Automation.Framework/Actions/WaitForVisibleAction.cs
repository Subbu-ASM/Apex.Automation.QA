using System;
using System.Threading;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;

namespace Automation.Framework.Actions
{
    public class WaitForVisibleAction : IAction
    {
        public void Execute(ActionContext context, TestStepModel step)
        {
            var timeout = TimeSpan.FromMilliseconds(step.TimeoutMs ?? 10000);
            var start = DateTime.Now;

            while ((DateTime.Now - start) < timeout)
            {
                if (context.UiDriver.IsVisible(context.CurrentPage, step.Target))
                    return;

                Thread.Sleep(300);
            }

            throw new TimeoutException(
                $"Element '{step.Target}' not visible on page '{context.CurrentPage}' after {timeout.TotalSeconds} seconds");
        }
    }
}

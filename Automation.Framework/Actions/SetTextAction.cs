using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;

namespace Automation.Framework.Actions
{
    public class SetTextAction : IAction
    {
        public void Execute(ActionContext context, TestStepModel step)
        {
            context.UiDriver.SetText(
                context.CurrentPage,
                step.Target,
                step.Value
            );
        }
    }
}

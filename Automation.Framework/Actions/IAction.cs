using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;

namespace Automation.Framework.Actions
{
    public interface IAction
    {
        void Execute(ActionContext context, TestStepModel step);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Core
{ 
    public static class WaitHelper
    {
        public static void Until(Func<bool> condition, TimeSpan timeout)
        {
            var start = DateTime.Now;

            while (DateTime.Now - start < timeout)
            {
                if (condition())
                    return;

                Thread.Sleep(300);
            }

            throw new TimeoutException($"Condition not met within {timeout.TotalSeconds} seconds.");
        }
    }

}

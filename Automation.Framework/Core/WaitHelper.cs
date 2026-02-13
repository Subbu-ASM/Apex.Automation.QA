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
        public static void Until(Func<bool> condition, int timeoutMs, int pollMs = 200)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (condition())
                    return;

                Thread.Sleep(pollMs);
            }

            throw new TimeoutException("Wait condition failed within timeout.");
        }
    }

}

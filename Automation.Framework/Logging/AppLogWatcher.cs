using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Logging
{
    public class AppLogWatcher
    {
        private readonly string _logFile;

        public AppLogWatcher(string logFile)
        {
            _logFile = logFile;
        }

        public bool Contains(string keyword)
        {
            if (!File.Exists(_logFile)) return false;
            return File.ReadAllText(_logFile).Contains(keyword);
        }

        public void WaitFor(string keyword, int timeoutMs)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (Contains(keyword)) return;
                Thread.Sleep(300);
            }
            throw new TimeoutException($"Log entry not found: {keyword}");
        }
    }

}

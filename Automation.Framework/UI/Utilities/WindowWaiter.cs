using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Linq;
using System.Threading;

namespace Automation.Framework.Utilities
{
    public static class WindowWaiter
    {
        public static AutomationElement WaitForMainWindow(Application app, UIA3Automation automation, string mainWindowAutomationId, int timeoutSeconds = 30)
        {
            var start = DateTime.Now;

            while ((DateTime.Now - start).TotalSeconds < timeoutSeconds)
            {
                var windows = app.GetAllTopLevelWindows(automation);

                var mainWindow = windows.FirstOrDefault(w =>
                {
                    try
                    {
                        return w.Properties.AutomationId.Value == mainWindowAutomationId;
                    }
                    catch
                    {
                        return false;
                    }
                });

                if (mainWindow != null)
                    return mainWindow;

                Thread.Sleep(500);
            }

            throw new TimeoutException($"Main window with AutomationId '{mainWindowAutomationId}' not found within {timeoutSeconds} seconds.");
        }
    }
}

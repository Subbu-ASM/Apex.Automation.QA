using Automation.Framework.Logging;
using Automation.Framework.Models;
using Automation.Framework.UI;
using System;

namespace Automation.Framework.Core
{
    public class StepEngine
    {
        private readonly IUiDriver _ui;
        private readonly AppLogWatcher _logWatcher;

        public StepEngine(IUiDriver ui, AppLogWatcher logWatcher)
        {
            _ui = ui;
            _logWatcher = logWatcher;
        }

        public void Execute(TestStep step)
        {
            switch (step.Action)
            {
                case "Click":
                    _ui.Click(step.Target);
                    break;

                case "SetText":
                    _ui.SetText(step.Target, step.Value);
                    break;

                case "SelectCombo":
                    _ui.SelectCombo(step.Target, step.Value);
                    break;

                case "WaitForVisible":
                    WaitHelper.Until(() => _ui.IsVisible(step.Target), step.TimeoutMs);
                    break;

                case "WaitForLog":
                    _logWatcher.WaitFor(step.Value, step.TimeoutMs);
                    break;

                case "ValidateText":
                    var actual = _ui.ReadText(step.Target);
                    if (actual != step.Expected)
                        throw new InvalidOperationException(
                            $"Validation failed. Expected: '{step.Expected}', Actual: '{actual}'");
                    break;

                default:
                    throw new NotSupportedException($"Unknown action: {step.Action}");
            }
        }
    }
}

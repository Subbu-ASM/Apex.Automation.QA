using Automation.Framework.Models;
using Automation.Framework.UI;
using System;

namespace Automation.Framework.Core
{
    public class StepEngine
    {
        private readonly IUiDriver _ui;

        public StepEngine(IUiDriver ui)
        {
            _ui = ui;
        }

        public void Execute(TestStep step)
        {
            switch (step.Action)
            {
                case "Click":
                    {
                        _ui.Click(step.Target);
                        break;
                    }

                case "SetText":
                    {
                        _ui.SetText(step.Target, step.Value);
                        break;
                    }

                case "SelectCombo":
                    {
                        _ui.SelectCombo(step.Target, step.Value);
                        break;
                    }

                case "ValidateText":
                    {
                        var actual = _ui.ReadText(step.Target);
                        if (actual != step.Expected)
                        {
                            throw new InvalidOperationException(
                                $"Validation failed for '{step.Target}'. Expected: '{step.Expected}', Actual: '{actual}'");
                        }
                        break;
                    }

                case "ValidateEnabled":
                    {
                        var enabled = _ui.IsEnabled(step.Target);
                        var expected = bool.Parse(step.Expected);
                        if (enabled != expected)
                        {
                            throw new InvalidOperationException(
                                $"ValidateEnabled failed for '{step.Target}'. Expected: '{expected}', Actual: '{enabled}'");
                        }
                        break;
                    }

                case "WaitForVisible":
                    {
                        WaitHelper.Until(() => _ui.IsVisible(step.Target), step.TimeoutMs);
                        break;
                    }

                case "WaitForLog":
                    {
                        _logWatcher.WaitFor(step.Value, step.TimeoutMs);
                        break;
                    }

                default:
                    throw new NotSupportedException($"Unknown action: {step.Action}");
            }
        }
    }
}
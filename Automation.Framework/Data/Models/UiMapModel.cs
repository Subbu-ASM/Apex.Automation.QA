using System.Collections.Generic;
using Automation.Framework.Data.Models;

namespace Automation.Framework.Data.Models
{
    public class UiMapModel
    {
        public Dictionary<string, Dictionary<string, string>> Pages { get; set; }
            = new();

        public string GetAutomationId(string page, string logicalName)
        {
            if (string.IsNullOrWhiteSpace(page))
                throw new InvalidOperationException("CurrentPage is NULL or empty");

            if (string.IsNullOrWhiteSpace(logicalName))
                throw new InvalidOperationException("Target is NULL or empty");

            if (!Pages.ContainsKey(page))
                throw new KeyNotFoundException($"Page '{page}' not found in UiMap.json");

            if (!Pages[page].ContainsKey(logicalName))
                throw new KeyNotFoundException(
                    $"Target '{logicalName}' not found in page '{page}'");

            return Pages[page][logicalName];
        }
    }
}
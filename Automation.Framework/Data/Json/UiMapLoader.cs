using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Automation.Framework.Data.Models;

namespace Automation.Framework.Data.Json
{
    public static class UiMapLoader
    {
        public static UiMapModel Load(string uiMapPath)
        {
            if (!File.Exists(uiMapPath))
            {
                throw new FileNotFoundException(
                    $"AutomationIds.json not found at path: {uiMapPath}");
            }

            string json = File.ReadAllText(uiMapPath);

            var uiMap = JsonSerializer.Deserialize<UiMapModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (uiMap == null)
            {
                throw new InvalidDataException("Failed to deserialize AutomationIds.json");
            }

            return uiMap;
        }
    }
}

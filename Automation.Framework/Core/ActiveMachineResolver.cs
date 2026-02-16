using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Automation.Framework.Core
{
    public static class ActiveMachineResolver
    {
        public static string GetActiveMachine()
        {
            var json = File.ReadAllText("active-machine.json");
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("ActiveMachine").GetString();
        }
    }
}

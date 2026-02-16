using Automation.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Automation.Framework.Core
{
    public static class MachineConfigLoader
    {
        public static MachineConfig Load(string configPath)
        {
            var json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<MachineConfig>(json)
                   ?? throw new InvalidOperationException("Invalid MachineConfig.json");
        }
    }
}

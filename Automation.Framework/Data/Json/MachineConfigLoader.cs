using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Automation.Framework.Data.Models;
using System.Text.Json;

namespace Automation.Framework.Data.Json
{
    public static class MachineConfigLoader
    {
        public static MachineConfig Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"MachineConfig not found at {path}");

            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<MachineConfig>(json)
                   ?? throw new InvalidDataException("Invalid MachineConfig.json");
        }
    }
}

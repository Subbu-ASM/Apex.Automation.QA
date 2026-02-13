using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Automation.Framework.Reporting
{
    public static class JsonResultWriter
    {
        public static void Write(string path, object result)
        {
            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }
    }
}

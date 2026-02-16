using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Data.Models
{
    public class MachineConfig
    {
        public string MachineName { get; set; } = string.Empty;

        // Path to WPF exe
        public string AppPath { get; set; } = string.Empty;

        // Path to UiMap.json
        public string UiMapPath { get; set; } = string.Empty;

        // Folder containing test case json files
        public string TestCaseRoot { get; set; } = string.Empty;

        // Root folder where results will be stored
        public string ResultRoot { get; set; } = string.Empty;

        // Optional: app log file path
        public string LogPath { get; set; } = string.Empty;
    }
}

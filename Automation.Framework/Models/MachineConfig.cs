using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Models
{
    public class MachineConfig
    {
        public string MachineName { get; set; }
        public string AppPath { get; set; }
        public string UiMapPath { get; set; }
        public string TestCaseRoot { get; set; }
        public string LogPath { get; set; }
        public string ResultRoot { get; set; }
    }
}

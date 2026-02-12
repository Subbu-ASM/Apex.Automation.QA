using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.UI
{
  
        public interface IUiDriver
        {
            void Click(string logicalName);
            string ReadText(string logicalName);
            bool IsEnabled(string logicalName);

            void SetText(string logicalName, string text);
            void SelectCombo(string logicalName, string itemText);
        }

    }


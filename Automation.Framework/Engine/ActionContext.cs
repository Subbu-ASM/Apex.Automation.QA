using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Data.Db;
using Automation.Framework.Data.Models;
using Automation.Framework.Engine;
using Automation.Framework.UI.Driver;

namespace Automation.Framework.Engine
{
    public class ActionContext
    {
        public UiMapModel UiMap { get; set; }
        public IUiDriver UiDriver { get; set; }
        public IDbService DbService { get; set; }

        public string CurrentPage { get; set; }

        public string ResultRoot { get; set; }
    }
}
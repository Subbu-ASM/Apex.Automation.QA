using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Data.Models;

namespace Automation.Framework.Data.Db
{
    public interface IDbService
    {
        string GetTestData(string key);

        void SaveTestResult(TestResultEntity result);
    }
}

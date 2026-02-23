using System;
using System.IO;

namespace Automation.Framework.Utilities
{
    public static class PathHelper
    {
        public static string GetSolutionRoot()
        {
            var dir = AppContext.BaseDirectory;

            while (dir != null &&
                   !File.Exists(Path.Combine(dir, "Apex.Automation.QA.sln")))
            {
                dir = Directory.GetParent(dir)?.FullName;
            }

            if (dir == null)
                throw new DirectoryNotFoundException("❌ Solution root not found. Ensure Apex.Automation.QA.sln exists.");

            return dir;
        }
    }
}
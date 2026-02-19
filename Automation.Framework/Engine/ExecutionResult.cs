
namespace Automation.Framework.Engine
{
    public class ExecutionResult
    {
        public string TestCaseId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public bool IsPassed { get; set; }
        public string FailureStep { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}




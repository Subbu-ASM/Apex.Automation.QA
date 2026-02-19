using Automation.Framework.Actions;
using Automation.Framework.Data.Models;
using System;

namespace Automation.Framework.Engine
{
    public class TestEngine
    {
        private readonly ActionContext _context;

        public TestEngine(ActionContext context)
        {
            _context = context;
        }

        public ExecutionResult Execute(TestCaseModel testCase)
        {
            var result = new ExecutionResult
            {
                TestCaseId = testCase.TestCaseId,
                TestName = testCase.TestName,
                IsPassed = true
            };

            _context.CurrentPage = testCase.Page;

            foreach (var step in testCase.Steps)
            {
                try
                {
                    IAction action = ActionDispatcher.GetAction(step.Action);
                    action.Execute(_context, step);
                }
                catch (Exception ex)
                {
                    result.IsPassed = false;
                    result.FailureStep = $"{step.Action} -> {step.Target}";
                    result.Message = ex.Message;
                    break;
                }
            }

            if (result.IsPassed)
            {
                result.Message = "Test executed successfully";
            }

            // Optional DB logging (only if DbService exists)
            if (_context.DbService != null)
            {
                _context.DbService.SaveTestResult(new TestResultEntity
                {
                    TestCaseId = result.TestCaseId,
                    TestName = result.TestName,
                    IsPassed = result.IsPassed,
                    FailureStep = result.FailureStep,
                    Message = result.Message,
                    ExecutedAt = DateTime.Now
                });
            }

            return result;
        }
    }
}

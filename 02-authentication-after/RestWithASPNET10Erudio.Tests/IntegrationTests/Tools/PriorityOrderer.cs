using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace RestWithASPNET10Erudio.Tests.IntegrationTests.Tools
{
    public class PriorityOrderer : ITestMethodOrderer
    {
        public IReadOnlyCollection<TTestMethod> OrderTestMethods<TTestMethod>
            (IReadOnlyCollection<TTestMethod> testMethods) where TTestMethod
            : notnull, ITestMethod
        {
            return testMethods
                .OrderBy(tm => (tm as IXunitTestMethod)?.Method
                    .GetCustomAttribute<TestPriorityAttribute>()?.Priority ?? 0)
                .ToList();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestPriorityAttribute : Attribute
    {
        public int Priority { get; }
        public TestPriorityAttribute(int priority)
            => Priority = priority;
    }
}

using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSpec;
using NSpec.Domain;

[TestClass]
public class DebuggerShim
{
    [TestMethod]
    public void debug()
    {
        var tagOrClassName = "describe_Plate";

        var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, tagOrClassName);

        var contexts = invocation.Run();

        //assert that there aren't any failures
        contexts.Failures().Count().should_be(0);
    }
}

using System.Collections.Generic;
using TechTalk.SpecFlow;
using Test.Tools;
using Test.Tools.DTO;

namespace Test.Scenarios.Hooks
{
  [Binding]
  public sealed class ScenarioHooks
  {
    private readonly ScenarioContext _scenarioContext;

    public ScenarioHooks(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void PrepareContainers()
    {
      _scenarioContext.Set(new List<ComputerDto>());
    }

    [AfterScenario]
    public void AfterScenario()
    {
      var browser = _scenarioContext.Get<Browser>();
      browser?.Dispose();
    }
  }
}
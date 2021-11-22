using TechTalk.SpecFlow;
using Test.Tools;

namespace Test.Scenarios.Steps
{
  [Binding]
  public class BaseStep
  {
    public const string SiteUrl = "http://computer-database.herokuapp.com/computers";

    protected readonly ScenarioContext ScenarioContext;

    protected BaseStep(ScenarioContext scenarioContext)
    {
      ScenarioContext = scenarioContext;
    }

    protected Browser CurrentBrowser => ScenarioContext.Get<Browser>();
  }
}
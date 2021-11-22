using OpenQA.Selenium;

namespace Test.Tools.WebElements
{
  /// <summary>
  ///   WebElement representation of a Label object.
  /// </summary>
  public class Label : WebElementBase
  {
    public Label(Browser browser, By locator) : base(browser, locator)
    {
    }

    public Label(WebElementBase parent, By locator) : base(parent, locator)
    {
    }

    public string GetText()
    {
      return WebElement.Text;
    }
  }
}
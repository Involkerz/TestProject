using OpenQA.Selenium;

namespace Test.Tools.WebElements
{
  public class Button : WebElementBase
  {
    public Button(Browser browser, By locator) : base(browser, locator)
    {
    }

    public Button(WebElementBase parent, By locator) : base(parent, locator)
    {
    }

    public virtual void Click()
    {
      WebElement.Click();
    }
  }
}
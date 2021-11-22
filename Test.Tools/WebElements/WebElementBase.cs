using OpenQA.Selenium;

namespace Test.Tools.WebElements
{
  /// <summary>
  ///   Basic custom WebElement class.
  /// </summary>
  public class WebElementBase
  {
    public WebElementBase(Browser browser, By locator)
    {
      Browser = browser;
      Locator = locator;
    }

    public WebElementBase(WebElementBase parent, By locator)
    {
      Browser = parent.Browser;
      Parent = parent;
      Locator = locator;
    }

    public Browser Browser { get; set; }
    public WebElementBase Parent { get; set; }
    public By Locator { get; set; }

    public IWebElement WebElement => GetWebElement();

    public IWebElement GetWebElement(bool withWait = true)
    {
      return Parent == default
        ? Browser.FindElement(Locator, withWait)
        : Browser.FindElement(Parent.WebElement, Locator, withWait);
    }

    public bool IsPresent()
    {
      try
      {
        GetWebElement();
        return true;
      }
      catch (NoSuchElementException)
      {
        return false;
      }

      catch (WebDriverTimeoutException)
      {
        return false;
      }
    }

    public bool IsNotPresent()
    {
      try
      {
        GetWebElement(false);
        return false;
      }
      catch (NoSuchElementException)
      {
        return true;
      }

      catch (WebDriverTimeoutException)
      {
        return true;
      }
    }
  }
}
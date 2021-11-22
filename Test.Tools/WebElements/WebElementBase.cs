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

    /// <summary>
    ///   Searches for <see cref="IWebElement" /> when action is being performed.
    /// </summary>
    public IWebElement WebElement => GetWebElement();

    /// <summary>
    ///   Searches for <see cref="IWebElement" /> withing the page or provided parent WebElement if any.
    /// </summary>
    /// <param name="withWait"></param>
    /// <returns></returns>
    public IWebElement GetWebElement(bool withWait = true)
    {
      return Parent == default
        ? Browser.FindElement(Locator, withWait)
        : Browser.FindElement(Parent.WebElement, Locator, withWait);
    }

    /// <summary>
    ///   Checks if <see cref="IWebElement" /> is present.
    ///   Will wait if element is not loaded.
    /// </summary>
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

    /// <summary>
    ///   Checks if <see cref="IWebElement" /> is not present.
    ///   Will not wait if element is not loaded. Saves time.
    /// </summary>
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
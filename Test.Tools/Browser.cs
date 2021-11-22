using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Test.Tools
{
  public class Browser
  {
    private IWebDriver _webDriver;

    public Browser()
    {
      _webDriver = new ChromeDriver();
      _webDriver.Manage().Window.Maximize();
    }

    public static TimeSpan DefaultTimeout => TimeSpan.FromSeconds(10);

    public WebDriverWait WebDriverWait(TimeSpan timeout = default)
    {
      return new WebDriverWait(_webDriver, timeout == default ? DefaultTimeout : timeout);
    }

    public void NavigateTo(string url)
    {
      _webDriver.Navigate().GoToUrl(url);
    }

    /// <summary>
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="withWait"></param>
    /// <returns></returns>
    public IWebElement FindElement(By selector, bool withWait = true)
    {
      if (withWait)
      {
        var wait = WebDriverWait();
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        wait.Until(driver => driver.FindElement(selector));
      }

      return _webDriver.FindElement(selector);
    }

    /// <summary>
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="selector"></param>
    /// <param name="withWait"></param>
    /// <returns></returns>
    public IWebElement FindElement(IWebElement parent, By selector, bool withWait = true)
    {
      if (withWait)
      {
        var wait = WebDriverWait();
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        wait.Until(driver => parent.FindElement(selector));
      }

      return parent.FindElement(selector);
    }

    /// <summary>
    ///   Finds all <see cref="IWebElement" /> matching the search criteria within provided element.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="selector"></param>
    /// <param name="withWait"></param>
    public ReadOnlyCollection<IWebElement> FindElements(IWebElement parent, By selector, bool withWait = true)
    {
      if (withWait)
      {
        var wait = WebDriverWait();
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        wait.Until(driver => parent.FindElements(selector));
      }

      return parent.FindElements(selector);
    }

    public void Dispose()
    {
      _webDriver?.Quit();
      _webDriver?.Dispose();
      _webDriver = null;
    }
  }
}
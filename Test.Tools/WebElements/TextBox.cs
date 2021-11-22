using OpenQA.Selenium;

namespace Test.Tools.WebElements
{
  /// <summary>
  ///   WebElement representation of a TextBox object.
  /// </summary>
  public class TextBox : WebElementBase
  {
    public TextBox(Browser browser, By locator) : base(browser, locator)
    {
    }

    public TextBox(WebElementBase parent, By locator) : base(parent, locator)
    {
    }

    public string GetText()
    {
      return WebElement.Text;
    }

    public void CleanUpText()
    {
      WebElement.Click();
      WebElement.SendKeys(Keys.Control + "a");
      WebElement.SendKeys(Keys.Delete);
    }

    /// <summary>
    ///   Set text data to the TextBox
    /// </summary>
    /// <param name="text">Data to set</param>
    /// <param name="cleanUp">Should we clean the text box before setting the data. Default = true</param>
    public virtual void SetText(string text, bool cleanUp = true)
    {
      if (cleanUp) CleanUpText();

      WebElement.SendKeys(text);
    }
  }
}
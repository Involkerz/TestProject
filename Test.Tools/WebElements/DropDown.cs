using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Tools.WebElements
{
  /// <summary>
  ///   WebElement representation of a DropdownBox object.
  /// </summary>
  public class DropDown : WebElementBase
  {
    public DropDown(Browser browser, By locator) : base(browser, locator)
    {
    }

    public DropDown(WebElementBase parent, By locator) : base(parent, locator)
    {
    }

    public void Select(string menuItem)
    {
      if (!string.IsNullOrEmpty(menuItem))
      {
        var selectElement = new SelectElement(WebElement);
        selectElement.SelectByText(menuItem);
      }
    }
  }
}
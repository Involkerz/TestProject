using OpenQA.Selenium;
using Test.Tools;
using Test.Tools.WebElements;

namespace Test.UI.Pages
{
  public class AddComputerForm : ComputerFormBase
  {
    public AddComputerForm(Browser currentBrowser) : base(currentBrowser)
    {
    }

    private Button AddButton => new Button(CurrentBrowser, By.XPath("//input[@class='btn primary']"));

    public void ClickAdd()
    {
      AddButton.Click();
    }
  }
}
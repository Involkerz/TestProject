using OpenQA.Selenium;
using Test.Tools;
using Test.Tools.WebElements;

namespace Test.UI.Pages
{
  public class EditComputerForm : ComputerFormBase
  {
    public EditComputerForm(Browser currentBrowser) : base(currentBrowser)
    {
    }

    private Button SaveButton => new Button(CurrentBrowser, By.XPath("//input[@class='btn primary']"));

    private Button DeleteButton => new Button(CurrentBrowser, By.XPath("//input[@class='btn danger']"));

    public void Delete()
    {
      DeleteButton.Click();
    }

    public void SaveChanges()
    {
      SaveButton.Click();
    }
  }
}
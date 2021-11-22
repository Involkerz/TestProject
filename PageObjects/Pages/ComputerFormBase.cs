using OpenQA.Selenium;
using Test.Tools;
using Test.Tools.DTO;
using Test.Tools.WebElements;

namespace Test.UI.Pages
{
  public class ComputerFormBase : BasePage
  {
    public ComputerFormBase(Browser currentBrowser) : base(currentBrowser)
    {
    }

    private TextBox ComputerNameTextBox => new TextBox(CurrentBrowser, By.Id("name"));
    private TextBox ComputerIntroducedDateTextBox => new TextBox(CurrentBrowser, By.Id("introduced"));
    private TextBox ComputerDiscontinuedDateTextBox => new TextBox(CurrentBrowser, By.Id("discontinued"));
    private DropDown ComputerCompanyNameDropdownBox => new DropDown(CurrentBrowser, By.Id("company"));

    private WebElementBase ComputerNameValidationHighlightZone => new Label(ComputerNameTextBox, By.XPath("./../.."));

    private WebElementBase ComputerIntroducedDateValidationHighlightZone =>
      new Label(ComputerIntroducedDateTextBox, By.XPath("./../.."));

    private WebElementBase ComputerDiscontinuedValidationHighlightZone =>
      new Label(ComputerDiscontinuedDateTextBox, By.XPath("./../.."));

    private Button CancelButton()
    {
      return new Button(CurrentBrowser, By.XPath("//input[@class='btn primary']"));
    }

    public void ConfigureComputerData(ComputerDto computerDto)
    {
      if (!string.IsNullOrEmpty(computerDto.ComputerName)) ComputerNameTextBox.SetText(computerDto.ComputerName);

      if (!string.IsNullOrEmpty(computerDto.Introduced)) ComputerIntroducedDateTextBox.SetText(computerDto.Introduced);

      if (!string.IsNullOrEmpty(computerDto.Discontinued))
        ComputerDiscontinuedDateTextBox.SetText(computerDto.Discontinued);

      if (!string.IsNullOrEmpty(computerDto.Company)) ComputerCompanyNameDropdownBox.Select(computerDto.Company);
    }

    public bool GetComputerNameValidationStatus()
    {
      return ComputerNameValidationHighlightZone.WebElement.GetAttribute("Class").Equals("clearfix error");
    }

    public bool GetComputerIntroducedDateValidationStatus()
    {
      return ComputerIntroducedDateValidationHighlightZone.WebElement.GetAttribute("Class").Equals("clearfix error");
    }

    public bool GetComputerDiscontinuedDateValidationStatus()
    {
      return ComputerDiscontinuedValidationHighlightZone.WebElement.GetAttribute("Class").Equals("clearfix error");
    }
  }
}
using OpenQA.Selenium;
using Test.Tools;
using Test.Tools.DTO;
using Test.Tools.WebElements;

namespace Test.UI.Pages
{
  public class ComputersPage : BasePage
  {
    public ComputersPage(Browser currentBrowser) : base(currentBrowser)
    {
    }

    private Button FilterByNameButton => new Button(CurrentBrowser, By.Id("searchsubmit"));
    private Button AddNewComputerButton => new Button(CurrentBrowser, By.Id("add"));

    private ComputersTable ComputersTable =>
      new ComputersTable(CurrentBrowser, By.XPath("//table[@class='computers zebra-striped']"));

    private TextBox FilterTextBox => new TextBox(CurrentBrowser, By.Id("searchbox"));
    private Label CounterLabel => new Label(CurrentBrowser, By.XPath("//section[@id='main']/h1"));
    private Label NotificationLabel => new Label(CurrentBrowser, By.XPath("//div[@class='alert-message warning']"));

    /// <summary>
    ///   Applies provided search criteria.
    /// </summary>
    public void ApplySearchByText(string searchCriteria)
    {
      FilterTextBox.SetText(searchCriteria);
      FilterByNameButton.Click();
    }

    /// <summary>
    ///   Resets the search criteria.
    /// </summary>
    public void ResetSearch()
    {
      FilterTextBox.SetText(string.Empty);
      FilterByNameButton.Click();
    }

    /// <summary>
    ///   Gets the total computer count based on 'Computers found' label.
    /// </summary>
    /// <returns></returns>
    public int GetComputersCount()
    {
      var counterText = CounterLabel.GetText();
      return int.Parse(counterText.Replace(" computers found", string.Empty));
    }

    /// <summary>
    ///   Gets the notification message if present.
    ///   Otherwise returns empty string.
    /// </summary>
    public string GetNotificationMessage()
    {
      return NotificationLabel.GetText();
    }

    /// <summary>
    ///   Adds new computer based on provided data.
    /// </summary>
    public void AddNewComputer(ComputerDto newComputerDto)
    {
      OpenAddNewComputerForm();
      var addComputerForm = new AddComputerForm(CurrentBrowser);
      addComputerForm.ConfigureComputerData(newComputerDto);
      addComputerForm.ClickAdd();
    }

    /// <summary>
    ///   Opens 'Add new computer' form
    /// </summary>
    public void OpenAddNewComputerForm()
    {
      AddNewComputerButton.Click();
    }

    /// <summary>
    ///   Checks if specified computer can be found on page.
    /// </summary>
    public bool IsComputerPresent(ComputerDto computerDto)
    {
      if (ComputersTable.IsNotPresent()) return false;

      return ComputersTable.CheckIfSpecifiedComputerPresent(computerDto);
    }

    /// <summary>
    ///   Deletes specified computer.
    /// </summary>
    public void DeleteComputer(ComputerDto computerDto)
    {
      ApplySearchByText(computerDto.ComputerName);
      ComputersTable.OpenComputerDetails(computerDto);
      var configureComputerForm = new EditComputerForm(CurrentBrowser);
      configureComputerForm.Delete();
    }

    public void EditComputerData(ComputerDto oldComputer, ComputerDto newComputer)
    {
      ApplySearchByText(oldComputer.ComputerName);
      ComputersTable.OpenComputerDetails(oldComputer);
      var configureComputerForm = new EditComputerForm(CurrentBrowser);
      configureComputerForm.ConfigureComputerData(newComputer);
      configureComputerForm.SaveChanges();
    }
  }
}
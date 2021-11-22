using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Test.Tools;
using Test.Tools.DTO;
using Test.UI.Constants;
using Test.UI.Pages;

namespace Test.Scenarios.Steps
{
  [Binding]
  public class ComputersStepDefinitions : BaseStep
  {
    protected ComputersStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    private AddComputerForm AddComputerForm => new AddComputerForm(CurrentBrowser);
    private ComputersPage ComputersPage => new ComputersPage(CurrentBrowser);

    private List<ComputerDto> ListOfComputers => ScenarioContext.Get<List<ComputerDto>>();
    private ComputerDto LastComputer => ListOfComputers.Last();

    [Given(@"User starts browser and navigates to the Computers page")]
    public void GivenUserStartsBrowserAndNavigatesToTheComputersPage()
    {
      var newBrowser = new Browser();
      newBrowser.NavigateTo(SiteUrl);
      ScenarioContext.Set(newBrowser);
    }

    [Given(@"User clicks Add a new computer on the Computers page")]
    public void UserClicksAddANewComputerOnTheComputersPage()
    {
      ComputersPage.OpenAddNewComputerForm();
    }

    [When(@"User configures new computer on Add New Computer page")]
    public void UserConfiguresNewComputerOnAddNewComputerPage(Table table)
    {
      var newComputerConfiguration = table.CreateInstance<ComputerDto>();

      if (newComputerConfiguration.ComputerName == "random")
        newComputerConfiguration.ComputerName = Guid.NewGuid().ToString();

      AddComputerForm.ConfigureComputerData(newComputerConfiguration);
    }

    [When(@"User deletes created computer on the Computers page")]
    public void UserDeletesCreatedComputerOnTheComputersPage()
    {
      var computer = LastComputer;
      ComputersPage.DeleteComputer(computer);
    }

    [When(@"User edits computer on the Computers page")]
    public void UserEditsComputerOnTheComputersPage(Table table)
    {
      var oldComputer = LastComputer;
      var newComputer = table.CreateInstance<ComputerDto>();

      if (newComputer.ComputerName == "random") newComputer.ComputerName = Guid.NewGuid().ToString();

      ComputersPage.EditComputerData(oldComputer, newComputer);
      ListOfComputers.Add(newComputer);
    }


    [When(@"User confirms new computer creation on Add New Computer page")]
    public void UserConfirmsNewComputerCreationOnAddNewComputerPage()
    {
      AddComputerForm.ClickAdd();
    }

    [Given(@"User adds new computer on the Computers page")]
    [When(@"User adds new computer on the Computers page")]
    public void UserAddsNewComputerOnTheComputersPage(Table table)
    {
      var computerDto = table.CreateInstance<ComputerDto>();
      if (computerDto.ComputerName == "random") computerDto.ComputerName = Guid.NewGuid().ToString();

      ComputersPage.AddNewComputer(computerDto);

      ListOfComputers.Add(computerDto);
    }

    [Then(@"User checks that '(.*)' notification message is present")]
    public void UserChecksThatNewComputerNotificationMessageIsPresent(string eventType)
    {
      var computerName = LastComputer.ComputerName;

      string expectedMessage = eventType switch
      {
        "add" => ErrorsConstants.ComputerAdded(computerName),
        "delete" => ErrorsConstants.ComputerDeleted,
        "edit" => ErrorsConstants.ComputerEdited(computerName),
        _ => throw new ArgumentException($"The provided notification message type is not supported: {eventType}")
      };

      string actualMessage = ComputersPage.GetNotificationMessage();
      actualMessage.Should().BeEquivalentTo(expectedMessage);
    }

    [Then(@"User checks that computer is present on the Computers page")]
    public void UserChecksThatNewComputerIsPresentOnTheComputersPage()
    {
      var computer = LastComputer;
      ComputersPage.ApplySearchByText(computer.ComputerName);
      ComputersPage.IsComputerPresent(computer).Should().BeTrue();
    }

    [Then(@"User checks that old computer is not present on the Computers page")]
    public void UserChecksThatOldComputerIsNotPresentOnTheComputersPage()
    {
      var computer = ListOfComputers.First();
      ComputersPage.ApplySearchByText(computer.ComputerName);
      ComputersPage.IsComputerPresent(computer).Should().BeFalse();
    }


    [Then(@"User checks that computer is not present on the Computers page")]
    public void UserChecksThatComputerIsNotPresentOnTheComputersPage()
    {
      var computer = LastComputer;
      ComputersPage.ApplySearchByText(computer.ComputerName);
      ComputersPage.IsComputerPresent(computer).Should().BeFalse();
    }

    [Then(@"User checks that all validation errors are present on Add New Computer page")]
    public void UserChecksThatAllValidationErrorsArePresentOnAddNewComputerPage()
    {
      var actualComputerNameValidationStatus = AddComputerForm.GetComputerNameValidationStatus();
      var actualComputerIntroducedValidationStatus = AddComputerForm.GetComputerIntroducedDateValidationStatus();
      var actualComputerDiscontinuedValidationStatus = AddComputerForm.GetComputerDiscontinuedDateValidationStatus();

      using (new AssertionScope())
      {
        actualComputerNameValidationStatus.Should().BeTrue();
        actualComputerIntroducedValidationStatus.Should().BeTrue();
        actualComputerDiscontinuedValidationStatus.Should().BeTrue();
      }
    }
  }
}
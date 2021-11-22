using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using Test.Tools.DTO;

namespace Test.Tools.WebElements
{
  /// <summary>
  ///   WebElement representation of a Table object.
  /// </summary>
  public class ComputersTable : WebElementBase
  {
    private readonly string _emptyValue = "-";
    private readonly string _tableDateFormat = "dd MMM yyyy";
    private readonly string _testDataDateFormat = "yyyy-MM-dd";

    public ComputersTable(Browser browser, By locator) : base(browser, locator)
    {
    }

    public ComputersTable(WebElementBase parent, By locator) : base(parent, locator)
    {
    }

    private By RowsLocator => By.XPath("//tbody//tr");
    private By ComputerNameLocator => By.XPath(".//td[1]");
    private By IntroducedLocator => By.XPath(".//td[2]");
    private By DiscontinuedLocator => By.XPath(".//td[3]");
    private By CompanyLocator => By.XPath(".//td[4]");

    public void OpenComputerDetails(ComputerDto computerDto)
    {
      computerDto.Introduced = computerDto.Introduced == ""
        ? _emptyValue
        : ConvertDate(computerDto.Introduced, _testDataDateFormat, _tableDateFormat);

      computerDto.Discontinued = computerDto.Discontinued == ""
        ? _emptyValue
        : ConvertDate(computerDto.Discontinued, _testDataDateFormat, _tableDateFormat);

      if (computerDto.Company == "") computerDto.Company = _emptyValue;

      var locator = By.XPath(
        $"{RowsLocator.Criteria}[td[1]//text()='{computerDto.ComputerName}' and td[2]//text()[contains(.,'{computerDto.Introduced}')] and td[3]//text()[contains(.,'{computerDto.Discontinued}')] and td[4]//text()[contains(.,'{computerDto.Company}')]]/td[1]//a");
      var element = Browser.FindElement(WebElement, locator);
      element.Click();
    }

    public bool CheckIfSpecifiedComputerPresent(ComputerDto computerDto)
    {
      var listOfComputers = GetListOfComputers();
      return listOfComputers.Any(x => x.ComputerName == computerDto.ComputerName
                                      && x.Introduced == computerDto.Introduced
                                      && x.Discontinued == computerDto.Discontinued
                                      && x.Company == computerDto.Company);
    }

    private ReadOnlyCollection<IWebElement> GetRows()
    {
      var dataRows = Browser.FindElements(WebElement, RowsLocator);
      return dataRows;
    }

    private List<ComputerDto> GetListOfComputers()
    {
      var rows = GetRows();

      return rows.Select(row => new ComputerDto
        {
          ComputerName = Browser.FindElement(row, ComputerNameLocator).Text,
          Introduced = Browser.FindElement(row, IntroducedLocator).Text == _emptyValue
            ? string.Empty
            : ConvertDate(Browser.FindElement(row, IntroducedLocator).Text, _tableDateFormat, _testDataDateFormat),
          Discontinued = Browser.FindElement(row, DiscontinuedLocator).Text == _emptyValue
            ? string.Empty
            : ConvertDate(Browser.FindElement(row, DiscontinuedLocator).Text, _tableDateFormat, _testDataDateFormat),
          Company = Browser.FindElement(row, CompanyLocator).Text == _emptyValue
            ? string.Empty
            : Browser.FindElement(row, CompanyLocator).Text
        })
        .ToList();
    }

    private string ConvertDate(string date, string initialFormat, string requiredFormat)
    {
      var dateTime = DateTime.ParseExact(date, initialFormat, CultureInfo.InvariantCulture);
      return dateTime.ToString(requiredFormat);
    }
  }
}
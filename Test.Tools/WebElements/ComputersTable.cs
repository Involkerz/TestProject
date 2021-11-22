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
    private const string EmptyValue = "-";
    private const string TableDateFormat = "dd MMM yyyy";
    private const string TestDataDateFormat = "yyyy-MM-dd";

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


    /// <summary>
    ///   Opens Computer details page for provided computer.
    /// </summary>
    public void OpenComputerDetails(ComputerDto computerDto)
    {
      computerDto.Introduced = computerDto.Introduced == ""
        ? EmptyValue
        : ConvertDate(computerDto.Introduced, TestDataDateFormat, TableDateFormat);

      computerDto.Discontinued = computerDto.Discontinued == ""
        ? EmptyValue
        : ConvertDate(computerDto.Discontinued, TestDataDateFormat, TableDateFormat);

      if (computerDto.Company == "") computerDto.Company = EmptyValue;

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

    /// <summary>
    ///   Gets the table data as a lis t of <see cref="ComputerDto" />.
    ///   Note: dates are converted into <see cref="TestDataDateFormat" /> format.
    /// </summary>
    /// <returns></returns>
    private List<ComputerDto> GetListOfComputers()
    {
      var rows = GetRows();

      return rows.Select(row => new ComputerDto
        {
          ComputerName = Browser.FindElement(row, ComputerNameLocator).Text,
          Introduced = Browser.FindElement(row, IntroducedLocator).Text == EmptyValue
            ? string.Empty
            : ConvertDate(Browser.FindElement(row, IntroducedLocator).Text, TableDateFormat, TestDataDateFormat),
          Discontinued = Browser.FindElement(row, DiscontinuedLocator).Text == EmptyValue
            ? string.Empty
            : ConvertDate(Browser.FindElement(row, DiscontinuedLocator).Text, TableDateFormat, TestDataDateFormat),
          Company = Browser.FindElement(row, CompanyLocator).Text == EmptyValue
            ? string.Empty
            : Browser.FindElement(row, CompanyLocator).Text
        })
        .ToList();
    }

    /// <summary>
    ///   Converts date between date formats used on different parts of UI.
    /// </summary>
    /// <param name="initialFormat">Used to parse the initial date.</param>
    /// <param name="targetFormat">Output date format</param>
    public static string ConvertDate(string date, string initialFormat, string targetFormat)
    {
      var dateTime = DateTime.ParseExact(date, initialFormat, CultureInfo.InvariantCulture);
      return dateTime.ToString(targetFormat);
    }
  }
}
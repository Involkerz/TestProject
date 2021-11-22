using Test.Tools;

namespace Test.UI.Pages
{
  public class BasePage
  {
    protected BasePage(Browser currentBrowser)
    {
      CurrentBrowser = currentBrowser;
    }

    public Browser CurrentBrowser { get; }
  }
}
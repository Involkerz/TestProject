namespace Test.UI.Constants
{
  public static class ErrorsConstants
  {
    public static string ComputerDeleted = "Done! Computer has been deleted";

    public static string ComputerAdded(string computerName)
    {
      return $"Done! Computer {computerName} has been created";
    }

    public static string ComputerEdited(string computerName)
    {
      return $"Done! Computer {computerName} has been updated";
    }
  }
}
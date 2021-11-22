using System;
using System.ComponentModel;
using System.Linq;

namespace Test.Tools
{
  public static class EnumHelper
  {
    public static string GetDescription(this Enum enumType)
    {
      var fieldName = enumType.ToString();

      var fieldInfo = enumType.GetType().GetField(fieldName);

      var attributes =
        (DescriptionAttribute[]) fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

      return attributes!.Any() ? attributes.First().Description : fieldName;
    }
  }
}
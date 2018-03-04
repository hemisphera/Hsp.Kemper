using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  internal static class ValueConverter
  {
    /// <summary>
    /// Converts a property value to a value for use in SysExMessage
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static object ConvertFromProperty(PropertyInfo property, object value)
    {
      if (property.PropertyType == typeof(bool))
      {
        var v = (bool) value;
        return v ? 1 : 0;
      }

      if (property.PropertyType == typeof(string))
      {
        return (string) value;
      }

      // todo
      return null;
    }

    public static object ConvertToProperty(PropertyInfo property, object value)
    {
      if (property.PropertyType == typeof(string))
        return (string) value;

      if (property.PropertyType == typeof(bool))
        return (int)value > 0;

      if (property.PropertyType == typeof(int))
        return (int) value;

      if (property.PropertyType == typeof(double))
      {
        var intValue = (int) value;
        var attr = property.GetCustomAttribute<NrpnParameterAttribute>();
        var range = attr.MaxValue - attr.MinValue;
        if (range <= 0)
          return intValue;
        return Math.Round((double) intValue / 16383 * range + attr.MinValue, 2);
      }

      throw new NotSupportedException(""); // todo: implement specific exception
    }

  }

}

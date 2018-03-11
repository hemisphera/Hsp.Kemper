using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver.Converters
{

  internal class ConverterCache
  {

    private static ConverterCache _instance;

    public static ConverterCache Instance => _instance ?? (_instance = new ConverterCache());


    private Dictionary<Type, IValueConverter> Storage { get; }


    private ConverterCache()
    {
      Storage = new Dictionary<Type, IValueConverter>();
    }


    public IValueConverter GetConverter(Type converterType)
    {
      if (converterType == null) return null;

      if (!Storage.TryGetValue(converterType, out var converter))
      {
        converter = (IValueConverter) Activator.CreateInstance(converterType);
        Storage.Add(converterType, converter);
      }

      return converter;
    }

    public object ConvertToMidi(PropertyInfo property, object value)
    {
      var attribute = property.GetCustomAttribute<NrpnParameterAttribute>();
      var converter = GetConverter(attribute?.Converter);
      var resultValue = converter != null ? converter.ConvertToMidi(value) : value;
      
      if (attribute != null && attribute.IsStringParameter)
      {
        var stringValue = (string) resultValue;
        return stringValue;
      }
      else
      {
        var intValue = (int) resultValue;
        return intValue;
      }
    }

    public object ConvertFromMidi(PropertyInfo property, object value)
    {
      var attribute = property.GetCustomAttribute<NrpnParameterAttribute>();
      var converter = GetConverter(attribute?.Converter);
      return converter != null ? converter.ConvertFromMidi(value) : value;
    }

  }

}

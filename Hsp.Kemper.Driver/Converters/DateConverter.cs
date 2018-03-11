using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hsp.Kemper.Driver.Converters
{

  public class DateConverter : IValueConverter
  {

    private const string Format = "yyyy-MM-dd HH:mm:ss";


    public object ConvertToMidi(object value)
    {
      var dt = (DateTime) value;
      return dt.ToString(Format, CultureInfo.InvariantCulture);
    }

    public object ConvertFromMidi(object value)
    {
      var str = (string) value;
      return DateTime.ParseExact(str, Format, CultureInfo.InvariantCulture);
    }

  }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver.Converters
{

  public class RatingConverter : IValueConverter
  {

    public object ConvertToMidi(object value)
    {
      var rating = (int) value;
      rating = rating.Limit(0, 5);
      return $"{rating}Me";
    }

    public object ConvertFromMidi(object value)
    {
      var rating = (string) value;
      return int.Parse(rating.Substring(0, 1));
    }

  }

}

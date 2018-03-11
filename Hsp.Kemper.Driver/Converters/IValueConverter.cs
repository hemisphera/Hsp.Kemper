using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver.Converters
{

  public interface IValueConverter
  {

    object ConvertToMidi(object value);

    object ConvertFromMidi(object value);

  }

}

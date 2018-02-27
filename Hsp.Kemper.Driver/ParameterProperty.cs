using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public class ParameterProperty
  {
    
    public byte Page { get; set; }
    
    public byte Address { get; set; }

    public double MinValue { get; set; }

    public double MaxValue { get; set; }

    public PropertyInfo Property { get; set; }

    public object Value { get; set; }

  }

}

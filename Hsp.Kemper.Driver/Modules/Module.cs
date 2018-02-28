using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public abstract class Module
  {

    public byte NrpnPageNo { get; }


    protected Module(byte nrpnPageNo)
    {
      NrpnPageNo = nrpnPageNo;
    }


    public void SetValueFromParameter(ParameterProperty pv, object value)
    {
      var targetParameter = GetParameterProperties()
        .FirstOrDefault(p => p.Page == pv.Page &&
                             p.Address == pv.Address);
      if (targetParameter == null)
        throw new Exception(""); // todo: implement specific exception

      var newValue = ValueConverter.ConvertToProperty(targetParameter.Property, value);
      targetParameter.Property.SetValue(this, newValue);
    }


    public ParameterProperty[] GetParameterProperties()
    {
      return GetType().GetProperties()
        .Select(p =>
        {
          var attr = p.GetCustomAttribute<NrpnParameterAttribute>();
          if (attr != null)
          {
            return new ParameterProperty
            {
              Property = p,
              Value = p.GetValue(this),
              MinValue = attr.MinValue,
              MaxValue = attr.MaxValue,
              Address = attr.NrpnAddress,
              Page = NrpnPageNo
            };
          }

          return null;
        })
        .Where(v => v != null)
        .ToArray();
    }


  }
}

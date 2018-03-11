using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hsp.Kemper.Driver.Converters;

namespace Hsp.Kemper.Driver
{

  public abstract class Module
  {

    public byte NrpnPageNo { get; }


    protected Module(byte nrpnPageNo)
    {
      NrpnPageNo = nrpnPageNo;
    }

    /*
    public void SetValueFromParameter(ParameterProperty pv, object value)
    {
      var stringParameter = pv.Property.PropertyType == typeof(string);
      var validParameters = GetParameterProperties()
        .Where(p => p.Page == pv.Page &&
                    p.Address == pv.Address);
      if (stringParameter)
        validParameters = validParameters.Where(p => p.Property.PropertyType == typeof(string));
      var targetParameter = validParameters.LastOrDefault();

      if (targetParameter == null)
        throw new Exception(""); // todo: implement specific exception

      var newValue = ValueConverter.ConvertToProperty(targetParameter.Property, value);
      targetParameter.Property.SetValue(this, newValue);
    }
    */

    public void SetValueFromSysExMessage(NrpnSysExMessage msg)
    {
      object value = null;
      var isStringValue = false;
      if (msg is WriteValueMessage writeValueMsg)
      {
        value = writeValueMsg.Value;
        isStringValue = true;
      }

      if (msg is WriteStringValueMessage writeStringValueMsg)
        value = writeStringValueMsg.Value;

      var property = GetPropertyByAddress(msg, isStringValue);
      if (property == null)
        return;

      if (value == null)
        return;
        //throw new Exception(""); // todo: implement specific exception

      property.SetValue(this, ConverterCache.Instance.ConvertFromMidi(property, value));
    }


    public PropertyInfo[] GetParameterProperties(bool stringValue)
    {
      return GetType().GetProperties()
        .Where(p =>
        {
          var attr = p.GetCustomAttribute<NrpnParameterAttribute>();
          if (attr?.IsStringParameter != stringValue)
            return false;
          return true;
        })
        .ToArray();
    }

    public PropertyInfo GetPropertyByAddress(NrpnAddress addr, bool isStringValue)
    {
      var properties = GetParameterProperties(isStringValue);
      return properties.FirstOrDefault(p =>
      {
        var attr = p.GetCustomAttribute<NrpnParameterAttribute>();
        if (attr == null) return false;
        return NrpnPageNo == addr.Page && attr.NrpnAddress == addr.Address;
      });
    }

    public PropertyInfo GetPropertyByAddress(byte page, byte address, bool isStringValue)
    {
      return GetPropertyByAddress(new NrpnAddress(page, address), isStringValue);
    }

    public PropertyInfo GetPropertyByAddress(NrpnSysExMessage msg, bool isStringValue)
    {
      return GetPropertyByAddress(msg.Page, msg.Address, isStringValue);
    }

  }
}

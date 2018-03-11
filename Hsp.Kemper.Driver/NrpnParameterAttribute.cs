using System;

namespace Hsp.Kemper.Driver
{

  public class NrpnParameterAttribute : Attribute
  {

    public bool IsStringParameter { get; }

    public byte NrpnAddress { get; }

    public double MinValue { get; set; }

    public double MaxValue { get; set; }

    public Type Converter { get; set; }


    public NrpnParameterAttribute(byte address, bool isStringParameter = false)
    {
      NrpnAddress = address;
      IsStringParameter = isStringParameter;
    }

  }

}
using System;

namespace Hsp.Kemper.Driver
{

  public class NrpnParameterAttribute : Attribute
  {

    public byte NrpnAddress { get; }

    public double MinValue { get; set; }

    public double MaxValue { get; set; }


    public NrpnParameterAttribute(byte address)
    {
      NrpnAddress = address;
    }

  }

}
using System;
using System.Linq;
using System.Text;

namespace Hsp.Kemper.Driver
{

  public class WriteStringValueMessage : NrpnSysExMessage
  {

    public static WriteStringValueMessage FromInboundData(byte[] dataBlock)
    {
      var page = dataBlock[8];
      var address = dataBlock[9];
      var value = dataBlock.Skip(10).TakeWhile(c => c != 0).ToArray();
      var str = Encoding.ASCII.GetString(value);
      return new WriteStringValueMessage(page, address, str);
    }


    public string Value { get; }


    public WriteStringValueMessage(byte page, byte address, string value) : base(page, address, KemperFunctions.WriteStringParameter)
    {
      Value = value;
    }


    protected override byte[] GetData()
    {
      return new[]
      {
        Page,
        Address,
      }.Concat(Encoding.ASCII.GetBytes(Value)).Concat(new byte [] { 0x00 }).ToArray();
    }

    public override string ToString()
    {
      return Value;
    }
  }

}

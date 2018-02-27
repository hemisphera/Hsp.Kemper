using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public abstract class SysExMessage
  {

    public static SysExMessage Parse(byte[] data)
    {
      var functioncode = data[6];
      if (functioncode == KemperFunctions.WriteSingleParameter)
        return WriteValueMessage.FromInboundData(data);
      if (functioncode == KemperFunctions.WriteStringParameter)
        return WriteStringValueMessage.FromInboundData(data);

      throw new NotSupportedException($"Function code {functioncode:X} is not supported.");
    }


    public static readonly byte[] ManufacturerId = { 0x00, 0x20, 0x33 };

    public static readonly byte[] ProductType = { 0x02 };

    public static readonly byte[] DeviceId = { 0x7f };

    public byte FunctionCode { get; }


    protected SysExMessage(byte functionCode)
    {
      FunctionCode = functionCode;
    }


    protected abstract byte[] GetData();

    public byte[] GetMessageContent()
    {
      const byte instanceNo = 0;
      return new [] { FunctionCode, instanceNo }.Concat(GetData()).ToArray();
    }

  }

}

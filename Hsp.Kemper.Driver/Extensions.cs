using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public static class Extensions
  {

    public static SysExMessage SendWithResult(this IMidiSysExDevice ifc, SysExMessage msg)
    {
      ifc.SendSysExMessage(msg);
      ifc.WaitForResult();
      var result = ifc.ReadSysExMessage();
      return result;
    }

    internal static byte[] To14BitArray(this int value)
    {
      var b1 = (byte)((value >> 7) & 0x7f);
      var b2 = (byte)(value & 0x7f);
      return new[] {b1, b2};
    }

    internal static int ToInt(this byte[] value)
    {
      if (value.Length != 2)
        throw new NotSupportedException($"The length of {nameof(value)} must be 2.");
      return value[0] << 7 | value[1];
    }

  }

}

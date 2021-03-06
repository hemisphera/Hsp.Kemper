﻿using System;
using System.CodeDom;
using System.IO;
using System.Reflection;
using System.Text;

namespace Hsp.Kemper.Driver
{

  public static class Extensions
  {

    public static SysExMessage SendWithResult(this IMidiDevice ifc, SysExMessage msg, TimeSpan timeout)
    {
      ifc.SendSysExMessage(msg);
      ifc.WaitForSysExMessage(timeout);
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

    internal static int Limit(this int value, int min, int max)
    {
      if (value < min) return min;
      if (value > max) return max;
      return value;
    }

    public static NrpnAddress GetNrpnAddress(this PropertyInfo prop, Module mod)
    {
      var attr = prop.GetCustomAttribute<NrpnParameterAttribute>();
      if (attr == null) return null;
      return new NrpnAddress(mod.NrpnPageNo, attr.NrpnAddress);
    }

  }

}

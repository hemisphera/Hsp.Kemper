using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hsp.Kemper.Driver.Converters;

namespace Hsp.Kemper.Driver
{

  public class WriteMultiValueMessage : NrpnSysExMessage
  {
    
    public static WriteMultiValueMessage FromModule(Module module)
    {
      var props = module.GetParameterProperties(false)
        .ToDictionary(
          p => p.GetNrpnAddress(module),
          p => p);

      if (!props.Any())
        throw new Exception(""); // todo: implement specific exception

      WriteMultiValueMessage msg = null;
      foreach (var kvp in props)
      {
        var prop = kvp.Value;
        var address = kvp.Key;
        if (msg == null)
          msg = new WriteMultiValueMessage(address.Page, address.Address);
        else
          msg.Values.Add((int) ConverterCache.Instance.ConvertToMidi(prop, prop.GetValue(module)));
      }

      return msg;
    }

    /// <summary>
    /// Creates a WriteMultiValueMessage from an inboud SysEx message byte[]
    /// </summary>
    /// <param name="dataBlock">The entire data block received from the MIDI SysEx message</param>
    /// <returns></returns>
    public static WriteMultiValueMessage FromInboundData(byte[] dataBlock)
    {
      var page = dataBlock[8];
      var address = dataBlock[9];
      var values = dataBlock.Skip(10).ToArray();

      var msg = new WriteMultiValueMessage(page, address);

      while (values.Length >= 2)
      {
        var ba = new[] {values[0], values[1]};
        msg.Values.Add(ba.ToInt());
        values = values.Skip(2).ToArray();
      }

      return msg;
    }


    public List<int> Values { get; set; }


    public WriteMultiValueMessage(byte page, byte address) : base(page, address, KemperFunctions.WriteMultiParameter)
    {
      Values = new List<int>();
    }

    protected override byte[] GetData()
    {
      return new[]
      {
        Page,
        Address
      }.Concat(BuildValueBlock()).ToArray();
    }

    private IEnumerable<byte> BuildValueBlock()
    {
      if (Values.Count > 64)
        throw new Exception(""); // todo: implement specific exception
      return Values.SelectMany(v => v.To14BitArray());
    }

  }

}

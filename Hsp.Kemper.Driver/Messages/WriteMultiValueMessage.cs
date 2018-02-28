using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hsp.Kemper.Driver
{

  public class WriteMultiValueMessage : NrpnSysExMessage
  {

    public static WriteMultiValueMessage FromModule(Module module)
    {
      var props = module.GetParameterProperties()
        .Where(p => p.Property.PropertyType != typeof(string))
        .OrderBy(p => p.Address)
        .ToArray();
      if (!props.Any())
        throw new Exception(""); // todo: implement specific exception

      var firstProp = props[0];
      var msg = new WriteMultiValueMessage(firstProp.Page, firstProp.Address);

      foreach (var prop in props)
        msg.Values.Add((int) ValueConverter.ConvertFromProperty(prop.Property, prop.Property.GetValue(module)));

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
      var values = dataBlock.Skip(9).ToArray();

      var msg = new WriteMultiValueMessage(page, address);

      while (values.Length > 0)
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

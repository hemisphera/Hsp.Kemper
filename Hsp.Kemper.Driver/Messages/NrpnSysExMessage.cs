using System.Reflection;
using System.Runtime.InteropServices;
using Hsp.Kemper.Driver.Converters;

namespace Hsp.Kemper.Driver
{

  public abstract class NrpnSysExMessage : SysExMessage
  {
    
    public byte Page { get; }
    
    public byte Address { get; }


    protected NrpnSysExMessage(byte page, byte address, byte functionCode) : base(functionCode)
    {
      Page = page;
      Address = address;
    }

    internal static NrpnSysExMessage CreateReadMessage(Module mod, PropertyInfo prop)
    {
      var attr = prop.GetCustomAttribute<NrpnParameterAttribute>();
      var addr = new NrpnAddress(mod.NrpnPageNo, attr.NrpnAddress);

      return
        attr.IsStringParameter ?
        (NrpnSysExMessage) new ReadStringValueMessage(addr.Page, addr.Address) :
        new ReadValueMessage(addr.Page, addr.Address);
    }

    internal static SysExMessage CreateWriteMessage(Module mod, PropertyInfo prop, object value)
    {
      var attr = prop.GetCustomAttribute<NrpnParameterAttribute>();
      var addr = new NrpnAddress(mod.NrpnPageNo, attr.NrpnAddress);

      var newValue = ConverterCache.Instance.ConvertToMidi(prop, value);
      return
        attr.IsStringParameter ?
        (NrpnSysExMessage) new WriteStringValueMessage(addr.Page, addr.Address, (string) newValue) :
        new WriteValueMessage(addr.Page, addr.Address, (int) newValue);
    }

  }

}

using System;

namespace Hsp.Kemper.Driver
{

  public class PropertyNotFoundException : Exception
  {

    public NrpnSysExMessage SysExMessage { get; }

    public Module Module { get; }


    public PropertyNotFoundException(Module mod, NrpnSysExMessage msg) : base(GetMessage(mod, msg))
    {
      SysExMessage = msg;
      Module = mod;
    }

    private static string GetMessage(Module mod, NrpnSysExMessage msg)
    {
      return $"No property was found for page {msg.Page}, address {msg.Address} on {mod.GetType()}";
    }
  }

}

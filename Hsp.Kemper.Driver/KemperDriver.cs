using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public class KemperDriver : IDisposable
  {

    public IMidiDevice Device { get; }

    public TimeSpan MidiTimeout { get; set; }

    public KemperRig CurrentRig { get; private set; }


    public KemperDriver(IMidiDevice device)
    {
      Device = device;

      MidiTimeout = TimeSpan.FromMilliseconds(1500);

      CurrentRig = new KemperRig();
    }


    public void WriteToDevice()
    {
      if (CurrentRig == null)
        throw new ArgumentNullException(nameof(CurrentRig));
      var props = CurrentRig.GetModuleProperties();
      foreach (var prop in props)
      {
        var module = (Module) prop.GetValue(CurrentRig);
        WriteToDevice(module);
      }
    }

    public void WriteToDevice(Module module)
    {
      var properties = module.GetParameterProperties(true);
      foreach (var property in properties)
      {
        var value = property.GetValue(this);
        var msg = NrpnSysExMessage.CreateWriteMessage(module, property, value);
        Device.SendSysExMessage(msg);
      }
    }

    public void ReadFromDevice()
    {
      CurrentRig = new KemperRig();

      var props = CurrentRig.GetModuleProperties();
      foreach (var prop in props)
      {
        var module = (Module) prop.GetValue(this);
        ReadFromDevice(module);
      }
    }

    public void ReadFromDevice(Module module)
    {
      var properties = module.GetParameterProperties(false);
      foreach (var property in properties)
      {
        var outMsg = NrpnSysExMessage.CreateReadMessage(module, property);
        var result = Device.SendWithResult(outMsg, MidiTimeout);
        if (result is NrpnSysExMessage inMsg)
          module.SetValueFromSysExMessage(inMsg);
      }
    }


    public void Dispose()
    {
      if (Device is IDisposable disp)
        disp.Dispose();
    }

  }

}

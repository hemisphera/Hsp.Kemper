using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public class KemperDriver : IDisposable
  {

    public IMidiSysExDevice Device { get; }

    
    public RigModule Rig { get; }

    public InputModule Input { get; }
    
    public AmplifierModule Amplifier { get; }


    public KemperDriver(IMidiSysExDevice device)
    {
      Device = device;

      Rig = new RigModule(this);
      Input = new InputModule(this);
      Amplifier = new AmplifierModule(this);
    }


    private IEnumerable<PropertyInfo> GetModuleProperties()
    {
      var props = GetType().GetProperties().Where(p => p.PropertyType.IsSubclassOf(typeof(Module)));
      return props;
    }


    public void Dispose()
    {
      if (Device is IDisposable disp)
        disp.Dispose();
    }

    public void WriteToDevice()
    {
      var props = GetModuleProperties();
      foreach (var prop in props)
      {
        var module = (Module) prop.GetValue(this);
        WriteToDevice(module);
      }
    }

    public void WriteToDevice(Module module)
    {
      var parameters = module.GetParameterProperties();
      foreach (var parameterValue in parameters)
      {
        var value = parameterValue.Property.GetValue(this);
        var msg = NrpnSysExMessage.CreateWriteMessage(parameterValue, value);
        Device.SendSysExMessage(msg);
      }
    }

    public void ReadFromDevice()
    {
      var props = GetModuleProperties();
      foreach (var prop in props)
      {
        var module = (Module) prop.GetValue(this);
        ReadFromDevice(module);
      }
    }

    public void ReadFromDevice(Module module)
    {
      var parameters = module.GetParameterProperties();
      foreach (var parameterValue in parameters)
      {
        var msg = NrpnSysExMessage.CreateReadMessage(parameterValue);
        var result = Device.SendWithResult(msg);

        object value = null;
        if (result is WriteValueMessage writeValueMsg)
          value = writeValueMsg.Value;
        if (result is WriteStringValueMessage writeStringValueMsg)
          value = writeStringValueMsg.Value;
        
        if (value == null)
          throw new Exception(""); // todo: implement specific exception
        
        module.SetValueFromParameter(parameterValue, value);
      }
    }

  }

}

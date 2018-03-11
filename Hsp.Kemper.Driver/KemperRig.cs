using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public class KemperRig
  {
    
    public RigModule Rig { get; }

    public InputModule Input { get; }
    
    public AmplifierModule Amplifier { get; }

    public EqualizerModule Equalizer { get; }
    
    public CabinetModule Cabinet { get; }

    public RigMetadataModule RigMetadata { get; }


    public KemperRig()
    {
      Rig = new RigModule();
      Input = new InputModule();
      Amplifier = new AmplifierModule();
      Equalizer = new EqualizerModule();
      Cabinet = new CabinetModule();
      RigMetadata = new RigMetadataModule();
    }

    
    public IEnumerable<PropertyInfo> GetModuleProperties()
    {
      var props = GetType().GetProperties().Where(p => p.PropertyType.IsSubclassOf(typeof(Module)));
      return props;
    }
    
    public Module GetModuleByPageNo(byte page)
    {
      return GetModuleProperties()
        .Select(p => p.GetValue(this)).OfType<Module>()
        .FirstOrDefault(m => m.NrpnPageNo == page);
    }

  }

}

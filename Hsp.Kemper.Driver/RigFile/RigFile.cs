using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hsp.Kemper.Driver
{

  public class RigFile
  {

    private KemperRigHeaderChunk Header { get; set; }

    private KemperRigTrackChunk Track { get; set; }


    public KemperRig Rig { get; private set; }


    public static RigFile Load(string filename)
    {
      using (var fs = File.OpenRead(filename))
        return Load(fs);
    }

    public static RigFile Load(Stream s)
    {
      var rf = new RigFile();

      while (s.Position != s.Length)
      {
        var chunk = MidiChunk.ReadNextChunk(s);
        if (chunk is KemperRigHeaderChunk header)
          rf.Header = header;
        if (chunk is KemperRigTrackChunk track)
          rf.Track = track;
      }
      
      rf.WritePropertiesToRig();
      return rf;
    }

    private void WritePropertiesToRig()
    {
      var modDict = new Dictionary<byte, Module>();

      Rig = new KemperRig();
      foreach (var msg in Track.Messages.OfType<NrpnSysExMessage>())
      {
        if (!modDict.TryGetValue(msg.Page, out var mod))
        {
          mod = Rig.GetModuleByPageNo(msg.Page);
          modDict.Add(mod.NrpnPageNo, mod);
        }
        mod.SetValueFromSysExMessage(msg);
      }
    }

  }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  [MidiChunk("KThd")]
  public class KemperRigHeaderChunk : MidiChunk
  {

    public int Format { get; private set; }

    public int NoOfTracks { get; private set; }
    
    public int Division { get; private set; }


    protected override void Parse(byte[] data)
    {
      Format = MidiFileHelper.ReadInt16(data.Take(2));
      NoOfTracks = MidiFileHelper.ReadInt16(data.Skip(2).Take(2));
      Division = MidiFileHelper.ReadInt16(data.Skip(4).Take(2));
    }

  }

}

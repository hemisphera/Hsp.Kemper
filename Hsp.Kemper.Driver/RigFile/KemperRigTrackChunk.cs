using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  [MidiChunk("KTrk")]
  public class KemperRigTrackChunk : MidiChunk
  {

    public SysExMessage[] Messages { get; private set; }

    protected override void Parse(byte[] data)
    {
      var ls = new List<object>();
      var eos = false;
      using (var ms = new MemoryStream(data))
      {
        while (!eos)
        {
          try
          {
            var mOffset = ReadVarLength(ms);
            var mEvent = ReadEvent(ms);
            ls.Add(SysExMessage.Parse(mEvent));
          }
          catch (NotSupportedException)
          {
          }
          eos = ms.Position == ms.Length;
        }
      }

      Messages = ls.OfType<SysExMessage>().ToArray();
    }

    private int ReadVarLength(Stream s)
    {
      var result = 0;
      var isLastByte = false;

      do
      {
        var val = s.ReadByte();
        isLastByte = (val & 128) != 128;
        result = result << 7 | val;
      } while (!isLastByte);

      return result;
    }

    private byte[] ReadEvent(Stream s)
    {
      var eventType = (byte)s.ReadByte();

      var isSysEx = eventType == 0xf0 || eventType == 0xf7;
      var dataLength = isSysEx ? ReadVarLength(s) : 5;

      var buffer = new byte[dataLength + 1];
      buffer[0] = eventType;
      s.Read(buffer, 1, buffer.Length - 1);
      return buffer;
    }

  }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace Hsp.Kemper.Driver
{

  public abstract class MidiChunk
  {

    private static Dictionary<string, Type> ChunkTypeMap { get; set; }

    public static MidiChunk ReadNextChunk(Stream str)
    {
      LoadChunkTypeMap();
      var chunkType = MidiFileHelper.ReadString(str, 4);
      var length = MidiFileHelper.ReadInt32(str);
      var buffer = new byte[length];
      str.Read(buffer, 0, buffer.Length);

      var chunk = (MidiChunk) Activator.CreateInstance(ChunkTypeMap[chunkType]);
      chunk.Type = chunkType;
      chunk.Length = length;
      chunk.Parse(buffer);
      return chunk;
    }

    private static void LoadChunkTypeMap()
    {
      if (ChunkTypeMap != null)
        return;
      ChunkTypeMap = 
        Assembly.GetExecutingAssembly()
          .GetTypes()
          .Where(t => t.IsSubclassOf(typeof(MidiChunk)))
          .ToDictionary(
             t => t.GetCustomAttribute<MidiChunkAttribute>().Name,
             t => t);
    }


    public string Type { get; private set; }
    
    public int Length { get; private set; }


    protected abstract void Parse(byte[] data);

  }

}

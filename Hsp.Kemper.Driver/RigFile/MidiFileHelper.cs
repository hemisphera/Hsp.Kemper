using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  internal static class MidiFileHelper
  {

    internal static int ReadInt16(Stream str)
    {
      var buffer = new byte[2];
      str.Read(buffer, 0, buffer.Length);
      return ReadInt16(buffer);
    }

    internal static int ReadInt16(IEnumerable<byte> buf)
    {
      var buffer = buf.ToArray();
      if (buffer.Length != 2)
        throw new NotSupportedException();
      Array.Reverse(buffer);
      return BitConverter.ToInt16(buffer, 0);
    }

    internal static int ReadInt32(Stream str)
    {
      var buffer = new byte[4];
      str.Read(buffer, 0, buffer.Length);
      return ReadInt32(buffer);
    }

    internal static int ReadInt32(IEnumerable<byte> buf)
    {
      var buffer = buf.ToArray();
      if (buffer.Length != 4)
        throw new NotSupportedException();
      Array.Reverse(buffer);
      return BitConverter.ToInt32(buffer, 0);
    }

    internal static string ReadString(Stream str, int length)
    {
      var buffer = new byte[length];
      str.Read(buffer, 0, length);
      return Encoding.ASCII.GetString(buffer);
    }

    internal static int ReadVLength(byte[] data, out int value)
    {
      var index = 0;
      var result = 0;

      do
      {
        var val = data[index];
        if ((val & 128) != 128)
          break;
        result = val << 7 | result & sbyte.MaxValue;
        index++;
      } while (index < data.Length);

      value = result;
      return index + 1;
    }

  }

}

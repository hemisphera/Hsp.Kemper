using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  [AttributeUsage(AttributeTargets.Class)]
  public class MidiChunkAttribute : Attribute
  {

    public string Name { get; }


    public MidiChunkAttribute(string name)
    {
      Name = name;
    }

  }

}

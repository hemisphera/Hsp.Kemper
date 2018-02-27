using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace Hsp.Kemper.Driver.Test
{
  class Program
  {
    static void Main(string[] args)
    {
      var od = new OutputDevice(1);
      var id = new InputDevice(0);
      var ifx = new SanfordMidiDevice(od, id);

      using (var kemper = new KemperDriver(ifx))
      {
        while (true)
        {
          kemper.ReadFromDevice();
          Console.WriteLine(kemper.Rig.RigTempo);
          Thread.Sleep(2500);
        }
      }

      //Console.ReadLine();
    }
  }
}

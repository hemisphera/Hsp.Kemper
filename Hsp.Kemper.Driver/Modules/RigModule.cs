using System;

namespace Hsp.Kemper.Driver
{

  public class RigModule : Module
  {

    [NrpnParameter(0, MinValue = 0.0, MaxValue = 256.0)]
    public double RigTempo { get; set; }

    [NrpnParameter(1)]
    public int RigVolume { get; set; }

    [NrpnParameter(2)]
    public bool RigTempoEnabled { get; set; }


    internal RigModule() : base(4)
    {
    }

  }

}

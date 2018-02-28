namespace Hsp.Kemper.Driver
{

  public class RigModule : Module
  {

    [NrpnParameter(0x00, MinValue = 0.0, MaxValue = 256.0)]
    public double RigTempo { get; set; }

    [NrpnParameter(0x01)]
    public int RigVolume { get; set; }

    [NrpnParameter(0x02)]
    public bool RigTempoEnabled { get; set; }


    internal RigModule() : base(4)
    {
    }

  }

}

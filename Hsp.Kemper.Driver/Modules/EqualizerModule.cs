namespace Hsp.Kemper.Driver
{

  public class EqualizerModule : Module
  {

    [NrpnParameter(2)]
    public bool Enabled { get; set; }

    [NrpnParameter(4)]
    public double Bass { get; set; }

    [NrpnParameter(5)]
    public double Middle { get; set; }

    [NrpnParameter(6)]
    public double Treble { get; set; }

    [NrpnParameter(7)]
    public double Presence { get; set; }


    public EqualizerModule() : base(11)
    {
    }

  }

}

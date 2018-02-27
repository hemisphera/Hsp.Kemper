namespace Hsp.Kemper.Driver
{

  public class AmplifierModule : Module
  {

    [NrpnParameter(2)]
    public bool Enabled { get; set; }

    [NrpnParameter(4)]
    public double Gain { get; set; }

    [NrpnParameter(6)]
    public double Definition { get; set; }

    [NrpnParameter(7)]
    public double Clarity { get; set; }

    [NrpnParameter(8)]
    public double PowerSagging { get; set; }

    [NrpnParameter(9)]
    public double Pick { get; set; }

    [NrpnParameter(10)]
    public double Compressor { get; set; }

    [NrpnParameter(11)]
    public double TubeShape { get; set; }

    [NrpnParameter(12)]
    public double TubeBias { get; set; }

    [NrpnParameter(13)]
    public double DirectMix { get; set; }


    public AmplifierModule(KemperDriver owner) : base(owner, 10)
    {
    }

  }

}

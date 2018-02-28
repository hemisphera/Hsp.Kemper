namespace Hsp.Kemper.Driver
{

  public class CabinetModule : Module
  {

    [NrpnParameter(2)]
    public bool Enabled { get; set; }

    [NrpnParameter(3)]
    public double Volume { get; set; }

    [NrpnParameter(4)]
    public double HighShift { get; set; }

    [NrpnParameter(5)]
    public double LowShift { get; set; }

    [NrpnParameter(6)]
    public double Character { get; set; }

    [NrpnParameter(7)]
    public bool PureCabinet { get; set; }


    public CabinetModule() : base(12)
    {
    }

  }

}

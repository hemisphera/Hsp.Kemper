namespace Hsp.Kemper.Driver
{

  public class InputModule : Module
  {

    [NrpnParameter(3)]
    public int NoiseGateIntensity { get; set; }

    [NrpnParameter(4)]
    public int InputCleaneSense { get; set; }
    
    [NrpnParameter(5)]
    public int InoutDistortionSense { get; set; }


    public InputModule(KemperDriver owner) : base(owner, 0x09)
    {
    }

  }

}

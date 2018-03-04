namespace Hsp.Kemper.Driver
{

  public class RigMetadataModule : Module
  {

    [NrpnParameter(1)]
    public string RigName {get; set; }

    [NrpnParameter(2)]
    public string RigAuthor {get; set; }

    [NrpnParameter(3)]
    public string Date {get; set; }

    [NrpnParameter(4)]
    public string LibraryName {get; set; }

    [NrpnParameter(5)]
    public string Editor {get; set; }

    [NrpnParameter(6)]
    public string x1 {get; set; }


    public RigMetadataModule() : base(0)
    {
    }

  }

}

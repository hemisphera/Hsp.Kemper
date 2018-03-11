using System;
using Hsp.Kemper.Driver.Converters;

namespace Hsp.Kemper.Driver
{

  public class RigMetadataModule : Module
  {

    [NrpnParameter(1, true)]
    public string Name {get; set; }

    [NrpnParameter(2, true)]
    public string Author {get; set; }

    [NrpnParameter(3, true, Converter = typeof(DateConverter))]
    public DateTime Date {get; set; }

    [NrpnParameter(4, true)]
    public string Comment {get; set; }

    [NrpnParameter(5, true)]
    public string Editor {get; set; }

    [NrpnParameter(6, true, Converter = typeof(RatingConverter))]
    public int MyRating {get; set; }

    [NrpnParameter(32, true)]
    public string CabinetName {get; set; }

    [NrpnParameter(33, true)]
    public string CabinetAuthor {get; set; }

    [NrpnParameter(36, true)]
    public string CabinetLocation {get; set; }

    [NrpnParameter(37, true)]
    public string CabinetManufacturer {get; set; }

    [NrpnParameter(38, true)]
    public string MicModel {get; set; }
    
    [NrpnParameter(39, true)]
    public string CabinetComment {get; set; }
    
    [NrpnParameter(40, true)]
    public string MicPosition {get; set; }
    
    [NrpnParameter(41, true)]
    public string SpeakerConfiguration {get; set; }

    [NrpnParameter(42, true)]
    public string CabinetModel {get; set; }
    
    [NrpnParameter(44, true)]
    public string SpeakerManufacturer {get; set; }

    [NrpnParameter(45, true)]
    public string SpeakerModel {get; set; }


    public RigMetadataModule() : base(0)
    {
    }

  }

}

namespace Hsp.Kemper.Driver
{

  public class ReadValueMessage : NrpnSysExMessage
  {

    public ReadValueMessage(byte page, byte address) : base(page, address, KemperFunctions.ReadSingleParameter)
    {
    }

    
    protected override byte[] GetData()
    {
      return new[]
      {
        Page,
        Address
      };
    }


  }

}
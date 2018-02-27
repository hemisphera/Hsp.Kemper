namespace Hsp.Kemper.Driver
{
  
  public class ReadStringValueMessage : NrpnSysExMessage
  {


    public ReadStringValueMessage(byte page, byte address) : base(page, address, KemperFunctions.ReadStringParameter)
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

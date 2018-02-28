namespace Hsp.Kemper.Driver
{

  public class ReadMultiValueMessage : NrpnSysExMessage
  {

    public ReadMultiValueMessage(byte page, byte address) : base(page, address, KemperFunctions.ReadMultiParameter)
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

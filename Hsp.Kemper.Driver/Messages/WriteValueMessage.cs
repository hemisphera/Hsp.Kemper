using System.Linq;

namespace Hsp.Kemper.Driver
{

  public class WriteValueMessage : NrpnSysExMessage
  {

    public static WriteValueMessage FromInboundData(byte[] dataBlock)
    {
      var page = dataBlock[8];
      var address = dataBlock[9];
      var value = dataBlock.Skip(9).Take(2).ToArray().From14BitArray();
      return new WriteValueMessage(page, address, value);
    }


    public int Value { get; }


    public WriteValueMessage(byte page, byte address, int value) : base(page, address, KemperFunctions.WriteSingleParameter)
    {
      Value = value;
    }


    protected override byte[] GetData()
    {
      return new[]
      {
        Page,
        Address,
      }.Concat(Value.To14BitArray()).ToArray();
    }

  }

}

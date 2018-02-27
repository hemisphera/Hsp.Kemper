namespace Hsp.Kemper.Driver
{

  internal static class KemperFunctions
  {

    public static readonly byte WriteSingleParameter = 0x01;
    
    public static readonly byte WriteMultiParameter = 0x02;
    
    public static readonly byte WriteExtParameter = 0x06;
    
    public static readonly byte WriteStringParameter = 0x03;
    
    public static readonly byte WriteBlobParameter = 0x04;

    public static readonly byte WriteExtStringParameter = 0x07;

    
    public static readonly byte ReadSingleParameter = 0x41;
    
    public static readonly byte ReadMultiParameter = 0x42;
    
    public static readonly byte ReadStringParameter = 0x43;
    
    public static readonly byte ReadExtStringParameter = 0x47;
    
    public static readonly byte ReadParameterAsString = 0x7c;

  }

}

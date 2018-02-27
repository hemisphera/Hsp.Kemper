namespace Hsp.Kemper.Driver
{

  public abstract class NrpnSysExMessage : SysExMessage
  {
    
    public byte Page { get; }
    
    public byte Address { get; }


    protected NrpnSysExMessage(byte page, byte address, byte functionCode) : base(functionCode)
    {
      Page = page;
      Address = address;
    }

    internal static SysExMessage CreateReadMessage(ParameterProperty prop)
    {
      if (prop.Property.PropertyType == typeof(string))
        return new ReadStringValueMessage(prop.Page, prop.Address);
      return new ReadValueMessage(prop.Page, prop.Address);
    }

    internal static SysExMessage CreateWriteMessage(ParameterProperty prop, object value)
    {
      if (prop.Property.PropertyType == typeof(string))
        return new WriteStringValueMessage(prop.Page, prop.Address, (string) value);
      return new WriteValueMessage(prop.Page, prop.Address, (int)value);
    }

  }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Kemper.Driver
{

  public class NrpnAddress : IComparable<NrpnAddress>
  {

    public byte Page { get; set; }
    
    public byte Address { get; set; }


    public NrpnAddress(byte page, byte address)
    {
      Page = page;
      Address = address;
    }

    public int CompareTo(NrpnAddress other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      var pageComparison = Page.CompareTo(other.Page);
      if (pageComparison != 0) return pageComparison;
      return Address.CompareTo(other.Address);
    }
  }

}

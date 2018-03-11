using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hsp.Kemper.Driver.Test
{

  public class Poller
  {

    public KemperDriver Driver { get; }

    public Poller(KemperDriver driver)
    {
      Driver = driver;
      driver.MidiTimeout = TimeSpan.FromMilliseconds(200);
    }

    public void Poll(bool strings = false)
    {
      var resultDoc = new XDocument();
      var rootElement = new XElement("Root");
      resultDoc.Add(rootElement);
      for (byte page = 0; page < 1; page++)
      {
        //if (new[] { 1, 2, 3}.Contains(page))
        //  continue;
        Console.WriteLine($"Trying page {page} of 127.");
        var pageElement = new XElement("Page", new XAttribute("No", page));
        rootElement.Add(pageElement);
        for (byte address = 0; address < 128; address++)
        {
          Console.WriteLine($"Trying address {address} of 127.");
          var addrElement = new XElement("Address", new XAttribute("No", address));
          var hasValue = false;

          if (!strings)
          {
            Thread.Sleep(200);
            var m1 = new ReadValueMessage(page, address);
            hasValue = WriteResult("Int", addrElement, m1, Driver.MidiTimeout);
          }
          else
          {
            Thread.Sleep(200);
            var m2 = new ReadStringValueMessage(page, address);
            hasValue = WriteResult("String", addrElement, m2, Driver.MidiTimeout);
          }

          if (hasValue)
          {
            pageElement.Add(addrElement);
            resultDoc.Save($@"d:\kemper-poll-{(strings ? "Strings" : "Int")}.xml");
          }
        }
      }
    }

    private bool WriteResult(string valReq, XElement addrElement, SysExMessage msg, TimeSpan to)
    {
      var r = Driver.Device.SendWithResult(msg, to);

      object result = null;

      if (r is WriteValueMessage wvm)
      {
        result = wvm.Value;
      }

      if (r is WriteStringValueMessage wsm)
      {
        result = wsm.Value;
      }

      if (String.IsNullOrEmpty(valReq) || result == null)
        return false;

      addrElement.Add(new XAttribute($"{valReq}Value", result));
      return true;
    }
  }

}

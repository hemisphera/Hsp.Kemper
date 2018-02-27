using System;

namespace Hsp.Kemper.Driver
{

  public interface IMidiSysExDevice
  {

    /// <summary>
    /// Read a single SysEx message from the device.
    /// </summary>
    /// <returns>The SysEx message</returns>
    SysExMessage ReadSysExMessage();

    /// <summary>
    /// Write a single SysEx message to the device.
    /// </summary>
    /// <param name="msg">The SysEx message</param>
    void SendSysExMessage(SysExMessage msg);

    /// <summary>
    /// Wait until the next SysEx message is available on the device.
    /// </summary>
    void WaitForResult();

  }

}
using System;

namespace Hsp.Kemper.Driver
{

  public interface IMidiDevice
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
    void WaitForSysExMessage(TimeSpan timeout);

    /// <summary>
    /// Send a control-change message
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="controller"></param>
    /// <param name="value"></param>
    void SendControlChange(byte channel, byte controller, byte value);

  }

}
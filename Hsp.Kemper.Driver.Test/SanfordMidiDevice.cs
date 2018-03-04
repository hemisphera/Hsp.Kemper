using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace Hsp.Kemper.Driver.Test
{

  public class SanfordMidiDevice : IMidiDevice, IDisposable
  {

    private Queue<SysExMessage> MessageQueue { get; }

    public OutputDevice OutputMidiDevice { get; }
    
    public InputDevice InputMidiDevice { get; }


    public SanfordMidiDevice(OutputDevice od, InputDevice id)
    {
      MessageQueue = new Queue<SysExMessage>();
      
      OutputMidiDevice = od;
      var outputDeviceName = OutputDevice.GetDeviceCapabilities(od.DeviceID).name;

      InputMidiDevice = id;
      var inputDeviceName = InputDevice.GetDeviceCapabilities(id.DeviceID).name;
      InputMidiDevice.StartRecording();
      InputMidiDevice.SysExMessageReceived += (s, e) => HandleMidiInMessage(e.Message);
    }

    private void HandleMidiInMessage(Sanford.Multimedia.Midi.SysExMessage msg)
    {
      var data = msg.GetBytes();
      var kmsg = SysExMessage.Parse(data);
      if (kmsg != null)
        lock (MessageQueue)
          MessageQueue.Enqueue(kmsg);
    }


    public SysExMessage ReadSysExMessage()
    {
      return MessageQueue.Any() ? MessageQueue.Dequeue() : null;
      /*
       * if (sem == null) return null;
      return SysExMessage.Parse(sem.GetMessageContent());
       */
    }

    public void SendSysExMessage(SysExMessage msg)
    {
      var sem = new Sanford.Multimedia.Midi.SysExMessage(
        new[] { (byte) SysExType.Start }
          .Concat(SysExMessage.ManufacturerId)
          .Concat(SysExMessage.ProductType)
          .Concat(SysExMessage.DeviceId)
          .Concat(msg.GetMessageContent())
          .Concat(new byte[] { 247 })
          .ToArray());
      OutputMidiDevice.Send(sem);
    }

    public void WaitForSysExMessage(TimeSpan timout)
    {
      var m = new ManualResetEvent(false);
      Task.Run(() =>
      {
        var hasItems = false;
        while (!hasItems)
        {
          lock (MessageQueue)
            hasItems = MessageQueue.Any();
          if (!hasItems)
            Thread.Sleep(125);
        }
        m.Set();
      });
      m.WaitOne(timout);
    }

    public void SendControlChange(byte channel, byte controller, byte value)
    {
      var msg = new ChannelMessage(ChannelCommand.Controller, channel, controller, value);
      OutputMidiDevice.Send(msg);
    }

    public void Dispose()
    {
      InputMidiDevice.StopRecording();
      InputMidiDevice.Close();
      InputMidiDevice.Dispose();

      OutputMidiDevice.Close();
      OutputMidiDevice.Dispose();
    }

  }

}

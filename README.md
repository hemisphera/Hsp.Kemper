# Hsp.Kemper
This is a C# library for accessing the Kemper Profiling Amplifier over MIDI SysEx commands. It is still a work in progress.

This library does not have any dependencies and deliberately does **not** implement MIDI communication itself. All MIDI communication is abstracted via the interface `IMidiSysExDevice` so you can choose which library you want to use.

A test project is included that uses `Sanford.Multimedia.Midi` (available on NuGet) as the MIDI interface.

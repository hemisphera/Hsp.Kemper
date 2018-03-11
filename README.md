# Hsp.Kemper
This is a C# library for accessing the Kemper Profiling Amplifier over MIDI SysEx commands. It is still a work in progress.

This library does not have any dependencies and deliberately does **not** implement MIDI communication itself. All MIDI communication is abstracted via the interface `IMidiDevice` so you can choose which MIDI library you want to use.

A sample implementation of `IMidiDevice` that uses `Sanford.Multimedia.Midi` (available on NuGet) is included.


BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | **FastDB**  | **244,788.1** |    **65 KB** |
 Insert5000  | FastDB  | 322,742.4 |   331 KB |
 Insert10000 | FastDB  | 339,592.6 |   663 KB |
 **Insert1000**  | **LiteDB**  |   **1,774.0** |   **136 KB** |
 Insert5000  | LiteDB  |   1,335.0 |   816 KB |
 Insert10000 | LiteDB  |   1,265.0 | 1,656 KB |
 **Insert1000**  | **SQLite**  |     **659.6** |    **52 KB** |
 Insert5000  | SQLite  |     683.4 |   228 KB |
 Insert10000 | SQLite  |     687.3 |   444 KB |

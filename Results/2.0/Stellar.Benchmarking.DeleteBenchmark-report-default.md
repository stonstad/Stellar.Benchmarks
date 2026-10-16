
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Delete1000**  | **FastDB**  | **393,416.8** |     **0 KB** |
 Delete5000  | FastDB  | 215,676.8 |     0 KB |
 Delete10000 | FastDB  | 356,389.3 |     0 KB |
 **Delete1000**  | **LiteDB**  |   **1,315.2** |   **192 KB** |
 Delete5000  | LiteDB  |   1,061.8 |   848 KB |
 Delete10000 | LiteDB  |   1,206.1 | 1,664 KB |
 **Delete1000**  | **SQLite**  |     **605.0** |    **52 KB** |
 Delete5000  | SQLite  |     625.6 |   228 KB |
 Delete10000 | SQLite  |     697.2 |   444 KB |

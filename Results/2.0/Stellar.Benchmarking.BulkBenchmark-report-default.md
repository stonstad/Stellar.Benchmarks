
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method    | Product | Op/s        | FileSize |
---------- |-------- |------------:|---------:|
 **Bulk1000**  | **FastDB**  |   **856,898.0** |    **60 KB** |
 Bulk5000  | FastDB  | 2,042,177.8 |   307 KB |
 Bulk10000 | FastDB  | 2,686,342.6 |   614 KB |
 **Bulk1000**  | **LiteDB**  |    **49,635.5** |     **8 KB** |
 Bulk5000  | LiteDB  |    43,337.3 |     8 KB |
 Bulk10000 | LiteDB  |    55,230.7 |     8 KB |
 **Bulk1000**  | **SQLite**  |   **191,588.0** |    **52 KB** |
 Bulk5000  | SQLite  |   290,898.4 |   228 KB |
 Bulk10000 | SQLite  |   303,469.6 |   444 KB |

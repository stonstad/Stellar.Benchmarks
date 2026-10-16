
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method     | Product | Op/s         | FileSize |
----------- |-------- |-------------:|---------:|
 **Query1000**  | **FastDB**  | **11,494,252.9** |    **65 KB** |
 Query5000  | FastDB  |  5,753,298.6 |   331 KB |
 Query10000 | FastDB  |  8,733,116.0 |   663 KB |
 **Query1000**  | **LiteDB**  |    **324,468.1** |   **136 KB** |
 Query5000  | LiteDB  |    328,272.8 |   816 KB |
 Query10000 | LiteDB  |    404,379.7 | 1,656 KB |
 **Query1000**  | **SQLite**  |  **2,082,032.1** |    **52 KB** |
 Query5000  | SQLite  |  1,009,693.1 |   228 KB |
 Query10000 | SQLite  |  1,892,529.6 |   444 KB |

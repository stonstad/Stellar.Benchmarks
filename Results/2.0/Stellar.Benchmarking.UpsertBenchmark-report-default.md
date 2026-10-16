
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | **FastDB**  | **332,354.7** |    **60 KB** |
 Upsert5000  | FastDB  | 297,746.7 |   307 KB |
 Upsert10000 | FastDB  | 315,830.7 |   614 KB |
 **Upsert1000**  | **LiteDB**  |   **3,259.8** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,693.9 |   848 KB |
 Upsert10000 | LiteDB  |   3,459.7 | 1,664 KB |
 **Upsert1000**  | **SQLite**  |     **713.0** |    **52 KB** |
 Upsert5000  | SQLite  |     645.0 |   228 KB |
 Upsert10000 | SQLite  |     678.5 |   444 KB |


BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | **FastDB**  | **104,574.4** |    **64 KB** |
 Upsert5000  | FastDB  | 100,457.9 |   326 KB |
 Upsert10000 | FastDB  |  96,655.6 |   653 KB |
 **Upsert1000**  | **LiteDB**  |   **3,528.3** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,563.4 |   848 KB |
 Upsert10000 | LiteDB  |   3,463.6 |  1664 KB |
 **Upsert1000**  | **SQLite**  |     **789.0** |    **52 KB** |
 Upsert5000  | SQLite  |     696.3 |   228 KB |
 Upsert10000 | SQLite  |     777.0 |   444 KB |
 **Upsert1000**  | **VistaDB** |   **4,770.7** |   **152 KB** |
 Upsert5000  | VistaDB |   3,414.3 |   632 KB |
 Upsert10000 | VistaDB |   2,364.2 |  1244 KB |


BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | **FastDB**  | **102,105.1** |    **64 KB** |
 Upsert5000  | FastDB  |  98,313.4 |   326 KB |
 Upsert10000 | FastDB  |  93,633.9 |   653 KB |
 **Upsert1000**  | **LiteDB**  |   **3,471.8** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,441.2 |   848 KB |
 Upsert10000 | LiteDB  |   3,192.2 | 1,664 KB |
 **Upsert1000**  | **SQLite**  |     **729.9** |    **52 KB** |
 Upsert5000  | SQLite  |     743.3 |   228 KB |
 Upsert10000 | SQLite  |     741.9 |   444 KB |
 **Upsert1000**  | **VistaDB** |   **4,935.4** |   **124 KB** |
 Upsert5000  | VistaDB |   3,442.2 |   484 KB |
 Upsert10000 | VistaDB |   2,372.8 |   940 KB |

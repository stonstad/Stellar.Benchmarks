
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 **Bulk1000**  | **FastDB**  | **217,646.8** |    **64 KB** |
 Bulk5000  | FastDB  | 226,483.1 |   326 KB |
 Bulk10000 | FastDB  | 226,075.9 |   653 KB |
 **Bulk1000**  | **LiteDB**  |  **52,103.5** |     **8 KB** |
 Bulk5000  | LiteDB  |  44,871.5 |     8 KB |
 Bulk10000 | LiteDB  |  44,219.3 |     8 KB |
 **Bulk1000**  | **SQLite**  | **196,256.7** |    **52 KB** |
 Bulk5000  | SQLite  | 290,778.0 |   228 KB |
 Bulk10000 | SQLite  | 294,455.4 |   444 KB |
 **Bulk1000**  | **VistaDB** |   **2,582.1** |   **136 KB** |
 Bulk5000  | VistaDB |   4,657.8 |   496 KB |
 Bulk10000 | VistaDB |   2,706.4 |   952 KB |

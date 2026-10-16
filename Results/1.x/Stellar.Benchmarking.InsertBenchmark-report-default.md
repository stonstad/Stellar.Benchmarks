
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | **FastDB**  | **193,550.9** |    **64 KB** |
 Insert5000  | FastDB  | 197,049.3 |   326 KB |
 Insert10000 | FastDB  | 192,855.8 |   653 KB |
 **Insert1000**  | **LiteDB**  |   **1,778.9** |   **136 KB** |
 Insert5000  | LiteDB  |   1,310.2 |   816 KB |
 Insert10000 | LiteDB  |   1,251.0 | 1,656 KB |
 **Insert1000**  | **SQLite**  |     **713.3** |    **52 KB** |
 Insert5000  | SQLite  |     747.6 |   228 KB |
 Insert10000 | SQLite  |     753.7 |   444 KB |
 **Insert1000**  | **VistaDB** |   **2,503.1** |   **124 KB** |
 Insert5000  | VistaDB |   4,078.1 |   484 KB |
 Insert10000 | VistaDB |   2,648.0 |   940 KB |

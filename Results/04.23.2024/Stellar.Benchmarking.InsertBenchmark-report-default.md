
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | **FastDB**  | **190,153.8** |    **64 KB** |
 Insert5000  | FastDB  | 198,371.8 |   326 KB |
 Insert10000 | FastDB  | 197,899.5 |   653 KB |
 **Insert1000**  | **LiteDB**  |   **1,852.8** |   **136 KB** |
 Insert5000  | LiteDB  |   1,333.5 |   816 KB |
 Insert10000 | LiteDB  |   1,300.4 |  1656 KB |
 **Insert1000**  | **SQLite**  |     **778.0** |    **52 KB** |
 Insert5000  | SQLite  |     747.7 |   228 KB |
 Insert10000 | SQLite  |     754.3 |   444 KB |
 **Insert1000**  | **VistaDB** |   **2,067.0** |   **152 KB** |
 Insert5000  | VistaDB |   4,006.3 |   632 KB |
 Insert10000 | VistaDB |   2,649.3 |  1244 KB |

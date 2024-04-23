
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 **Bulk1000**  | **FastDB**  | **217,766.9** |    **64 KB** |
 Bulk5000  | FastDB  | 231,333.3 |   326 KB |
 Bulk10000 | FastDB  | 227,601.0 |   653 KB |
 **Bulk1000**  | **LiteDB**  |  **51,565.1** |     **8 KB** |
 Bulk5000  | LiteDB  |  47,163.2 |     8 KB |
 Bulk10000 | LiteDB  |  44,020.6 |     8 KB |
 **Bulk1000**  | **SQLite**  | **209,027.2** |    **52 KB** |
 Bulk5000  | SQLite  | 286,072.8 |   228 KB |
 Bulk10000 | SQLite  | 301,032.2 |   444 KB |
 **Bulk1000**  | **VistaDB** |   **2,421.1** |   **164 KB** |
 Bulk5000  | VistaDB |   4,539.2 |   644 KB |
 Bulk10000 | VistaDB |   2,638.8 |  1256 KB |

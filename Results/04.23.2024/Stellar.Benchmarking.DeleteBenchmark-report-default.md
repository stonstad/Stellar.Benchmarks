
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s        | FileSize |
------------ |-------- |------------:|---------:|
 **Delete1000**  | **FastDB**  |   **180,522.3** |    **64 KB** |
 Delete5000  | FastDB  |   185,082.8 |   326 KB |
 Delete10000 | FastDB  |   166,645.6 |   653 KB |
 **Delete1000**  | **LiteDB**  |     **1,314.6** |   **192 KB** |
 Delete5000  | LiteDB  |     1,273.4 |   848 KB |
 Delete10000 | LiteDB  |     1,223.5 |  1664 KB |
 **Delete1000**  | **SQLite**  |   **814,022.9** |    **52 KB** |
 Delete5000  | SQLite  | 3,683,693.5 |   228 KB |
 Delete10000 | SQLite  | 7,311,188.6 |   444 KB |
 **Delete1000**  | **VistaDB** |    **24,819.0** |   **152 KB** |
 Delete5000  | VistaDB |    50,501.8 |   632 KB |
 Delete10000 | VistaDB |    40,984.0 |  1244 KB |

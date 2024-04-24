
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Delete1000**  | **FastDB**  | **181,408.1** |    **64 KB** |
 Delete5000  | FastDB  | 179,938.7 |   326 KB |
 Delete10000 | FastDB  | 164,177.6 |   653 KB |
 **Delete1000**  | **LiteDB**  |   **1,337.3** |   **192 KB** |
 Delete5000  | LiteDB  |   1,256.5 |   848 KB |
 Delete10000 | LiteDB  |   1,207.7 | 1,664 KB |
 **Delete1000**  | **SQLite**  |     **741.9** |    **52 KB** |
 Delete5000  | SQLite  |     748.4 |   228 KB |
 Delete10000 | SQLite  |     757.6 |   444 KB |
 **Delete1000**  | **VistaDB** |   **5,182.7** |   **124 KB** |
 Delete5000  | VistaDB |   6,537.4 |   484 KB |
 Delete10000 | VistaDB |   5,503.5 |   940 KB |

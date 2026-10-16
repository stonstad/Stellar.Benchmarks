
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method     | Product | Op/s         | FileSize |
----------- |-------- |-------------:|---------:|
 **Query1000**  | **FastDB**  | **14,992,503.7** |    **64 KB** |
 Query5000  | FastDB  |  9,416,787.0 |   326 KB |
 Query10000 | FastDB  | 12,080,699.1 |   653 KB |
 **Query1000**  | **LiteDB**  |    **249,787.7** |   **136 KB** |
 Query5000  | LiteDB  |    446,071.4 |   816 KB |
 Query10000 | LiteDB  |    497,798.9 | 1,656 KB |
 **Query1000**  | **SQLite**  |  **2,200,704.2** |    **52 KB** |
 Query5000  | SQLite  |  1,553,824.5 |   228 KB |
 Query10000 | SQLite  |  2,227,601.5 |   444 KB |
 **Query1000**  | **VistaDB** |     **74,732.6** |   **124 KB** |
 Query5000  | VistaDB |    418,324.9 |   484 KB |
 Query10000 | VistaDB |    574,299.0 |   940 KB |

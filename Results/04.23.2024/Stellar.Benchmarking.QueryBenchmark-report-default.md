
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method     | Product | Op/s         | FileSize |
----------- |-------- |-------------:|---------:|
 **Query1000**  | **FastDB**  | **16,384,489.4** |    **64 KB** |
 Query5000  | FastDB  | 15,760,441.3 |   326 KB |
 Query10000 | FastDB  | 17,304,031.8 |   653 KB |
 **Query1000**  | **LiteDB**  |    **246,702.4** |   **136 KB** |
 Query5000  | LiteDB  |    443,043.8 |   816 KB |
 Query10000 | LiteDB  |    478,668.9 |  1656 KB |
 **Query1000**  | **SQLite**  |  **6,625,441.7** |    **52 KB** |
 Query5000  | SQLite  |  8,368,200.8 |   228 KB |
 Query10000 | SQLite  | 11,097,547.4 |   444 KB |
 **Query1000**  | **VistaDB** |     **75,939.6** |   **152 KB** |
 Query5000  | VistaDB |    417,167.3 |   632 KB |
 Query10000 | VistaDB |    514,034.0 |  1244 KB |

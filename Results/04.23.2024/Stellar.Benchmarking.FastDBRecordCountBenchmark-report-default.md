
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method        | Product | Mode   | Op/s      | FileSize |
-------------- |-------- |------- |----------:|---------:|
 **Insert1000**    | **FastDB**  | **Bulk**   | **223,490.3** |    **36 KB** |
 Insert10000   | FastDB  | Bulk   | 232,011.4 |   370 KB |
 Insert100000  | FastDB  | Bulk   | 307,438.2 |  4084 KB |
 Insert1000000 | FastDB  | Bulk   | 296,788.1 | 48479 KB |
 **Insert1000**    | **FastDB**  | **Single** | **197,494.5** |    **36 KB** |
 Insert10000   | FastDB  | Single | 202,579.8 |   370 KB |
 Insert100000  | FastDB  | Single | 246,310.4 |  4084 KB |
 Insert1000000 | FastDB  | Single | 239,156.3 | 48479 KB |

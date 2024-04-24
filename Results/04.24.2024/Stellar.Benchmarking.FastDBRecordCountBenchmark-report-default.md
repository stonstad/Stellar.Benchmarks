
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method        | Product | Mode   | Op/s      | FileSize |
-------------- |-------- |------- |----------:|---------:|
 **Insert1000**    | **FastDB**  | **Bulk**   | **222,819.7** |    **36 KB** |
 Insert10000   | FastDB  | Bulk   | 231,856.1 |   370 KB |
 Insert100000  | FastDB  | Bulk   | 307,370.7 |  4084 KB |
 Insert1000000 | FastDB  | Bulk   | 294,974.6 | 48479 KB |
 **Insert1000**    | **FastDB**  | **Single** | **188,786.1** |    **36 KB** |
 Insert10000   | FastDB  | Single | 192,850.3 |   370 KB |
 Insert100000  | FastDB  | Single | 238,873.6 |  4084 KB |
 Insert1000000 | FastDB  | Single | 236,358.7 | 48479 KB |

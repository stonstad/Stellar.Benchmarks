
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-XPXJBA : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | FastDB  | 124,763.3 | 20096 KB |
 LargeEncrypted                   | FastDB  |  95,370.7 | 20205 KB |
 LargeEncryptedCompressed         | FastDB  |  63,603.0 | 14892 KB |
 LargeEncryptedCompressedParallel | FastDB  | 124,720.8 | 14892 KB |

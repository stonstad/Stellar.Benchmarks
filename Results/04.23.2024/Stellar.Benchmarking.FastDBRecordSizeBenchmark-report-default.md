
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WOUFLB : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


 Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | FastDB  | 140,470.9 | 20096 KB |
 LargeEncrypted                   | FastDB  | 100,435.0 | 20205 KB |
 LargeEncryptedCompressed         | FastDB  |  68,064.7 | 14892 KB |
 LargeEncryptedCompressedParallel | FastDB  | 138,588.9 | 14892 KB |

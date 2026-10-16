
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | FastDB  | 155,678.5 | 20106 KB |
 LargeEncrypted                   | FastDB  | 127,715.9 | 20214 KB |
 LargeEncryptedCompressed         | FastDB  |  89,838.0 | 14902 KB |
 LargeEncryptedCompressedBuffered | FastDB  | 121,830.2 | 14899 KB |

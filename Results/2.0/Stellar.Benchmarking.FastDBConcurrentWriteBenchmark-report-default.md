
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 Writers1  | FastDB  | 441,919.0 | 7,013 KB |
 Writers4  | FastDB  | 424,274.3 | 6,924 KB |
 Writers16 | FastDB  | 416,501.3 | 6,966 KB |

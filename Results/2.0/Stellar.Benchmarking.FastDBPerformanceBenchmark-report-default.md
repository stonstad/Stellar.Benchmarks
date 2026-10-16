
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method   | Product | Op/s        | FileSize |
--------- |-------- |------------:|---------:|
 Default  | FastDB  |   325,095.1 |   663 KB |
 Contract | FastDB  |   324,891.2 |   379 KB |
 Buffered | FastDB  | 1,524,452.2 |   343 KB |

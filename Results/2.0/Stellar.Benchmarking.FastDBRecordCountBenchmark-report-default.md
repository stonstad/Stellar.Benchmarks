
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7950X 4.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 11.0.100-rc.1.26425.128
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4
  Job-TASYDQ : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v4


 Method       | Product | Mode   | Op/s        | FileSize |
------------- |-------- |------- |------------:|---------:|
 **Insert1000**   | **FastDB**  | **Bulk**   |   **948,166.9** |    **37 KB** |
 Insert10000  | FastDB  | Bulk   | 3,303,673.7 |   379 KB |
 Insert100000 | FastDB  | Bulk   | 5,183,056.9 |  4181 KB |
 **Insert1000**   | **FastDB**  | **Single** |   **265,188.7** |    **37 KB** |
 Insert10000  | FastDB  | Single |   339,479.0 |   379 KB |
 Insert100000 | FastDB  | Single |   425,479.9 |  4181 KB |

# Stellar.Benchmarks
 Benchmarks for .NET Embedded Databases

# Benchmark Configuration

## Software
- Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
- .NET 8.0.4 (8.0.424.16909)
- X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

## Hardware
- AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
- Western Digital SN850X NVMe Gen4 PCIe SSD 2TB

Each benchmark receives a warm-up phase followed by three iterations for calculation of error and standard deviation.

# Products

 Product | License | Package | Version |
------------ |-------- |----------:|----------:|
[FastDB]()  | MIT License  | [Stellar.FastDB](https://www.nuget.org/packages/Stellar.FastDB) | 1.01 |
[LiteDB](https://www.litedb.org/)  | MIT License  | [LiteDB](https://www.nuget.org/packages/LiteDB) | 5.0.19 |
[SQLite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)  | MIT License  | [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite/) | 8.0.4 |
[VistaDB](https://vistadb.com/)  | Commercial | Unavailable | 6.3 |

# Results

https://github.com/stonstad/Stellar.Benchmarks/tree/main/Results

## Insert

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | **FastDB**  | **190,153.8** |    **64 KB** |
 Insert5000  | FastDB  | 198,371.8 |   326 KB |
 Insert10000 | FastDB  | 197,899.5 |   653 KB |
 **Insert1000**  | **LiteDB**  |   **1,852.8** |   **136 KB** |
 Insert5000  | LiteDB  |   1,333.5 |   816 KB |
 Insert10000 | LiteDB  |   1,300.4 |  1656 KB |
 **Insert1000**  | **SQLite**  |     **778.0** |    **52 KB** |
 Insert5000  | SQLite  |     747.7 |   228 KB |
 Insert10000 | SQLite  |     754.3 |   444 KB |
 **Insert1000**  | **VistaDB** |   **2,067.0** |   **152 KB** |
 Insert5000  | VistaDB |   4,006.3 |   632 KB |
 Insert10000 | VistaDB |   2,649.3 |  1244 KB |


## Delete

 Method      | Product | Op/s        | FileSize |
------------ |-------- |------------:|---------:|
 **Delete1000**  | **FastDB**  |   **180,522.3** |    **64 KB** |
 Delete5000  | FastDB  |   185,082.8 |   326 KB |
 Delete10000 | FastDB  |   166,645.6 |   653 KB |
 **Delete1000**  | **LiteDB**  |     **1,314.6** |   **192 KB** |
 Delete5000  | LiteDB  |     1,273.4 |   848 KB |
 Delete10000 | LiteDB  |     1,223.5 |  1664 KB |
 **Delete1000**  | **SQLite**  |   **814,022.9** |    **52 KB** |
 Delete5000  | SQLite  | 3,683,693.5 |   228 KB |
 Delete10000 | SQLite  | 7,311,188.6 |   444 KB |
 **Delete1000**  | **VistaDB** |    **24,819.0** |   **152 KB** |
 Delete5000  | VistaDB |    50,501.8 |   632 KB |
 Delete10000 | VistaDB |    40,984.0 |  1244 KB |


## Upsert

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | **FastDB**  | **104,574.4** |    **64 KB** |
 Upsert5000  | FastDB  | 100,457.9 |   326 KB |
 Upsert10000 | FastDB  |  96,655.6 |   653 KB |
 **Upsert1000**  | **LiteDB**  |   **3,528.3** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,563.4 |   848 KB |
 Upsert10000 | LiteDB  |   3,463.6 |  1664 KB |
 **Upsert1000**  | **SQLite**  |     **789.0** |    **52 KB** |
 Upsert5000  | SQLite  |     696.3 |   228 KB |
 Upsert10000 | SQLite  |     777.0 |   444 KB |
 **Upsert1000**  | **VistaDB** |   **4,770.7** |   **152 KB** |
 Upsert5000  | VistaDB |   3,414.3 |   632 KB |
 Upsert10000 | VistaDB |   2,364.2 |  1244 KB |

## Bulk

 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 **Bulk1000**  | **FastDB**  | **217,766.9** |    **64 KB** |
 Bulk5000  | FastDB  | 231,333.3 |   326 KB |
 Bulk10000 | FastDB  | 227,601.0 |   653 KB |
 **Bulk1000**  | **LiteDB**  |  **51,565.1** |     **8 KB** |
 Bulk5000  | LiteDB  |  47,163.2 |     8 KB |
 Bulk10000 | LiteDB  |  44,020.6 |     8 KB |
 **Bulk1000**  | **SQLite**  | **209,027.2** |    **52 KB** |
 Bulk5000  | SQLite  | 286,072.8 |   228 KB |
 Bulk10000 | SQLite  | 301,032.2 |   444 KB |
 **Bulk1000**  | **VistaDB** |   **2,421.1** |   **164 KB** |
 Bulk5000  | VistaDB |   4,539.2 |   644 KB |
 Bulk10000 | VistaDB |   2,638.8 |  1256 KB |

 ## Query

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

Queries records and returns a corresponding dataset.  
- FastDB and LiteDB: Customers.Where(a => a.Name.StartsWith("John") && a.Telephone > 5555555)
- SQLite and VistaDB: "SELECT * FROM Customers WHERE Name LIKE 'John%' AND Telephone > 5555555"


## FastDB Insert Performance

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

 ## FastDB Serialization and Parallelism

  Method   | Product | Op/s      | FileSize |
--------- |-------- |----------:|---------:|
 Default  | FastDB  | 198,628.3 |   653 KB |
 Contract | FastDB  | 201,109.0 |   370 KB |
 Parallel | FastDB  | 217,215.0 |   370 KB |

 ## FastDB Storage Size

  Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | FastDB  | 140,470.9 | 20096 KB |
 LargeEncrypted                   | FastDB  | 100,435.0 | 20205 KB |
 LargeEncryptedCompressed         | FastDB  |  68,064.7 | 14892 KB |
 LargeEncryptedCompressedParallel | FastDB  | 138,588.9 | 14892 KB |

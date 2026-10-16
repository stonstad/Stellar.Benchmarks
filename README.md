# Stellar.Benchmarks
 Benchmarks for .NET Embedded Databases and Persistent Storage

# Why do these benchmarks exist?
As a game developer, I needed a high-concurrency storage for player-managed game servers. Installing a local database is an unreasonable demand for players. I found that the available storage solutions were either too slow or struggled with concurrency during high volumes of simultaneous reads and writes. Such limitations are impractical for game servers, which must support a large number of players efficiently. I created a survey of available embedded solutions for C#/.NET. Full disclosure: I am the creator of [Stellar.FastDB](https://github.com/stonstad/Stellar.FastDB).

# Benchmark Configuration

## Software
- Windows 11 (10.0.26200.9711/25H2/2025Update/HudsonValley2)
- .NET 10.0.10 (10.0.10, 10.0.1026.32716)
- X64 RyuJIT x86-64-v4
- BenchmarkDotNet 0.15.8

## Hardware
- AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
- Western Digital SN850X NVMe Gen4 PCIe SSD 2TB

Each benchmark undergoes a warm-up iteration, followed by three additional iterations to calculate the benchmark's error and standard deviation. The complete source code for each test is available in this repository.

# Products

Each product is evaluated using its default settings.

 Product | License | Package | Version |
------------ |-------- |----------:|----------:|
[Stellar.FastDB](https://github.com/stonstad/Stellar.FastDB)  | MIT License  | [Stellar.FastDB](https://www.nuget.org/packages/Stellar.FastDB) | 2.0.0 |
[LiteDB](https://www.litedb.org/)  | MIT License  | [LiteDB](https://www.nuget.org/packages/LiteDB) | 5.0.19 |
[SQLite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)  | MIT License  | [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite/) | 8.0.4 |
[VistaDB](https://vistadb.com/)  | Commercial | Unavailable | 6.3 |

# Results

## Configuration
The test evaluates insert, delete, upsert, and querying performance using a [customer record](https://github.com/stonstad/Stellar.Benchmarks/blob/main/Test%20Data/Customer.cs). 

Relational embedded databases (such as SQLite) write records directly to table memory. In contrast, document-oriented databases (such as VistaDB and FastDB) serialize each record before storing it. VistaDB uses BSON serialization and FastDB may be configured to use UTF8 JSON or MessagePack binary. Unless noted otherwise, FastDB uses MessagePack. 

[Complete test results](https://github.com/stonstad/Stellar.Benchmarks/tree/main/Results) are available within this repository.

## Insert

The benchmarks involve inserting batches of 1K, 5K, and 10K records, with each operation focusing on an insert. After each benchmark, the size of the database file is recorded; FastDB's file is compacted first, as its background compaction would do once writes pause. Testing larger datasets, such as 500K records, with LiteDB, SQLite, and VistaDB is impractical due to the extended duration of these tests. 

FastDB and SQLite provide the smallest storage footprints among the databases tested. Notably, FastDB performs inserts significantly faster than its counterparts.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | <ins>FastDB</ins>  | **244,788.1** |    **65 KB** |
 Insert5000  | <ins>FastDB</ins>  | 322,742.4 |   331 KB |
 Insert10000 | <ins>FastDB</ins>  | 339,592.6 |   663 KB |
 **Insert1000**  | LiteDB  |   **1,774.0** |   **136 KB** |
 Insert5000  | LiteDB  |   1,335.0 |   816 KB |
 Insert10000 | LiteDB  |   1,265.0 | 1,656 KB |
 **Insert1000**  | SQLite  |     **659.6** |    **52 KB** |
 Insert5000  | SQLite  |     683.4 |   228 KB |
 Insert10000 | SQLite  |     687.3 |   444 KB |
 **Insert1000**  | VistaDB |   **2,503.1** |   **124 KB** |
 Insert5000  | VistaDB |   4,078.1 |   484 KB |
 Insert10000 | VistaDB |   2,648.0 |   940 KB |

## Delete

Deletes 1K, 5K, and 10K records. Among the databases evaluated, FastDB consistently achieves the fastest deletion times.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Delete1000**  | <ins>FastDB</ins>  | **393,416.8** |     **0 KB** |
 Delete5000  | <ins>FastDB</ins>  | 215,676.8 |     0 KB |
 Delete10000 | <ins>FastDB</ins>  | 356,389.3 |     0 KB |
 **Delete1000**  | LiteDB  |   **1,315.2** |   **192 KB** |
 Delete5000  | LiteDB  |   1,061.8 |   848 KB |
 Delete10000 | LiteDB  |   1,206.1 | 1,664 KB |
 **Delete1000**  | SQLite  |     **605.0** |    **52 KB** |
 Delete5000  | SQLite  |     625.6 |   228 KB |
 Delete10000 | SQLite  |     697.2 |   444 KB |
 **Delete1000**  | VistaDB |   **5,182.7** |   **124 KB** |
 Delete5000  | VistaDB |   6,537.4 |   484 KB |
 Delete10000 | VistaDB |   5,503.5 |   940 KB |

## Upsert

Upserts 1K, 5K, and 10K records.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | <ins>FastDB</ins>  | **332,354.7** |    **60 KB** |
 Upsert5000  | <ins>FastDB</ins>  | 297,746.7 |   307 KB |
 Upsert10000 | <ins>FastDB</ins>  | 315,830.7 |   614 KB |
 **Upsert1000**  | LiteDB  |   **3,259.8** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,693.9 |   848 KB |
 Upsert10000 | LiteDB  |   3,459.7 | 1,664 KB |
 **Upsert1000**  | SQLite  |     **713.0** |    **52 KB** |
 Upsert5000  | SQLite  |     645.0 |   228 KB |
 Upsert10000 | SQLite  |     678.5 |   444 KB |
 **Upsert1000**  | VistaDB |   **4,935.4** |   **124 KB** |
 Upsert5000  | VistaDB |   3,442.2 |   484 KB |
 Upsert10000 | VistaDB |   2,372.8 |   940 KB |

## Bulk

Bulk inserts 1K, 5K, or 10K records. Almost all tested solutions demonstrate significant performance gains during bulk-insert operations. While VistaDB lacks a standard bulk insert API, it provides alternative methods for efficient data insertion, such as DDA (Direct Data Access) and CLR (Common Language Runtime) procedures.

 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 **Bulk1000**  | <ins>FastDB</ins>  | **856,898.0** |    **60 KB** |
 Bulk5000  | <ins>FastDB</ins>  | 2,042,177.8 |   307 KB |
 Bulk10000 | <ins>FastDB</ins>  | 2,686,342.6 |   614 KB |
 **Bulk1000**  | LiteDB  |  **49,635.5** |     **8 KB** |
 Bulk5000  | LiteDB  |  43,337.3 |     8 KB |
 Bulk10000 | LiteDB  |  55,230.7 |     8 KB |
 **Bulk1000**  | SQLite  | **191,588.0** |    **52 KB** |
 Bulk5000  | SQLite  | 290,898.4 |   228 KB |
 Bulk10000 | SQLite  | 303,469.6 |   444 KB |
 **Bulk1000**  | VistaDB |   **2,582.1** |   **136 KB** |
 Bulk5000  | VistaDB |   4,657.8 |   496 KB |
 Bulk10000 | VistaDB |   2,706.4 |   952 KB |

 ## Query

Queries against a dataset of 1K, 5K, or 10K records and constructs a Customer instance for each result.

Queries records and returns a corresponding dataset.  
- FastDB and LiteDB: Customers.Where(a => a.Name.StartsWith("John") && a.Telephone > 5555555)
- SQLite and VistaDB: "SELECT * FROM Customers WHERE Name LIKE 'John%' AND Telephone > 5555555"
 
 Method     | Product | Op/s         | FileSize |
----------- |-------- |-------------:|---------:|
 **Query1000**  | <ins>FastDB</ins>  | **11,494,252.9** |    **65 KB** |
 Query5000  | <ins>FastDB</ins>  |  5,753,298.6 |   331 KB |
 Query10000 | <ins>FastDB</ins>  |  8,733,116.0 |   663 KB |
 **Query1000**  | LiteDB  |    **324,468.1** |   **136 KB** |
 Query5000  | LiteDB  |    328,272.8 |   816 KB |
 Query10000 | LiteDB  |    404,379.7 | 1,656 KB |
 **Query1000**  | SQLite  |  **2,082,032.1** |    **52 KB** |
 Query5000  | SQLite  |  1,009,693.1 |   228 KB |
 Query10000 | SQLite  |  1,892,529.6 |   444 KB |
 **Query1000**  | VistaDB |     **74,732.6** |   **124 KB** |
 Query5000  | VistaDB |    418,324.9 |   484 KB |
 Query10000 | VistaDB |    574,299.0 |   940 KB |



# Additional FastDB Benchmarks

The benchmarks presented here focus on FastDB's unique capabilities. FastDB effortlessly accomodates large record counts through a combination of fast insertions and a compact storage footprint.

## FastDB Insert Performance

 Method        | Product | Mode   | Op/s      | FileSize |
-------------- |-------- |------- |----------:|---------:|
 **Insert1000**    | <ins>FastDB</ins>  | **Single** | **265,188.7** |    **37 KB** |
 Insert10000   | <ins>FastDB</ins>  | Single | 339,479.0 |   379 KB |
 Insert100000  | <ins>FastDB</ins>  | Single | 425,479.9 |  4181 KB |
 **Insert1000**    | <ins>FastDB</ins>  | **Bulk**   | **948,166.9** |    **37 KB** |
 Insert10000   | <ins>FastDB</ins>  | Bulk   | 3,303,673.7 |   379 KB |
 Insert100000  | <ins>FastDB</ins>  | Bulk   | 5,183,056.9 |  4181 KB |


 ## FastDB Serialization and Buffering

The default out-of-the-box configuration for FastDB does not include serialization contracts or buffered writes. Serialization contracts produce the smallest files. Buffered writes return without waiting for the write to the file, which significantly increases throughput; the trade-off is that writes still queued are lost if the process crashes.
 
 Method   | Product | Op/s      | FileSize |
--------- |-------- |----------:|---------:|
 Default 10000  | <ins>FastDB</ins>  | 325,095.1 |   663 KB |
 Contract 10000 | <ins>FastDB</ins>  | 324,891.2 |   379 KB |
 Buffered 10000 | <ins>FastDB</ins>  | 1,524,452.2 |   343 KB |

 ## FastDB Large Records

When dealing with large records, which is often the case with serialized object graphs, the computational cost of compression and encryption can be substantial. In this benchmark, a significant amount of Lorem Ipsum text is stored both with and without encryption or compression. Enabling both compression and encryption markedly reduces throughput. Buffered writes, which return without waiting for the write to the file, recover much of that cost.

 Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | <ins>FastDB</ins>  | 155,678.5 | 20106 KB |
 LargeEncrypted                   | <ins>FastDB</ins>  | 127,715.9 | 20214 KB |
 LargeEncryptedCompressed         | <ins>FastDB</ins>  |  89,838.0 | 14902 KB |
 LargeEncryptedCompressedBuffered | <ins>FastDB</ins>  | 121,830.2 | 14899 KB |

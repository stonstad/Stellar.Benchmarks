# Stellar.Benchmarks
 Benchmarks for .NET Embedded Databases and Persistent Storage

# Why do these benchmarks exist?
As a game developer, I needed a high-concurrency storage for player-managed game servers. Installing a local database is an unreasonable demand for players. I found that the available storage solutions were either too slow or struggled with concurrency during high volumes of simultaneous reads and writes. Such limitations are impractical for game servers, which must support a large number of players efficiently. I created a survey of available embedded solutions for C#/.NET. Full disclosure: I am the creator of [Stellar.FastDB](https://github.com/stonstad/Stellar.FastDB).

# Benchmark Configuration

## Software
- Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
- .NET 8.0.4 (8.0.424.16909)
- X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

## Hardware
- AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
- Western Digital SN850X NVMe Gen4 PCIe SSD 2TB

Each benchmark undergoes a warm-up iteration, followed by three additional iterations to calculate the benchmark's error and standard deviation. The complete source code for each test is available in this repository.

# Products

Each product is evaluated using its default settings.

 Product | License | Package | Version |
------------ |-------- |----------:|----------:|
[Stellar.FastDB](https://github.com/stonstad/Stellar.FastDB)  | MIT License  | [Stellar.FastDB](https://www.nuget.org/packages/Stellar.FastDB) | 1.0.3 |
[LiteDB](https://www.litedb.org/)  | MIT License  | [LiteDB](https://www.nuget.org/packages/LiteDB) | 5.0.19 |
[SQLite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)  | MIT License  | [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite/) | 8.0.4 |
[VistaDB](https://vistadb.com/)  | Commercial | Unavailable | 6.3 |

# Results

The test evaluates insert, delete, upsert, and querying performance using a [customer record](https://github.com/stonstad/Stellar.Benchmarks/blob/main/Test%20Data/Customer.cs). For relational embedded databases like VistaDB and SQLite, serialization is not used. Document-oriented databases such as VistaDB and FastDB use serialization. VistaDB employs BSON serialization and FastDB may be configured to use UTF8 JSON or MessagePack binary. [Complete test results](https://github.com/stonstad/Stellar.Benchmarks/tree/main/Results) are available within this repository.

## Insert

The benchmarks involve inserting batches of 1K, 5K, and 10K records, with each operation focusing on an insert. After each benchmark, the size of the database file is recorded. Testing larger datasets, such as 500K records, with LiteDB, SQLite, and VistaDB is impractical due to the extended duration of these tests. 

FastDB and SQLite provide the smallest storage footprints among the databases tested. Notably, FastDB performs inserts significantly faster than its counterparts.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Insert1000**  | **FastDB**  | **193,550.9** |    **64 KB** |
 Insert5000  | FastDB  | 197,049.3 |   326 KB |
 Insert10000 | FastDB  | 192,855.8 |   653 KB |
 **Insert1000**  | **LiteDB**  |   **1,778.9** |   **136 KB** |
 Insert5000  | LiteDB  |   1,310.2 |   816 KB |
 Insert10000 | LiteDB  |   1,251.0 | 1,656 KB |
 **Insert1000**  | **SQLite**  |     **713.3** |    **52 KB** |
 Insert5000  | SQLite  |     747.6 |   228 KB |
 Insert10000 | SQLite  |     753.7 |   444 KB |
 **Insert1000**  | **VistaDB** |   **2,503.1** |   **124 KB** |
 Insert5000  | VistaDB |   4,078.1 |   484 KB |
 Insert10000 | VistaDB |   2,648.0 |   940 KB |

## Delete

Deletes 1K, 5K, and 10K records. Among the databases evaluated, FastDB consistently achieves the fastest deletion times.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Delete1000**  | **FastDB**  | **181,408.1** |    **64 KB** |
 Delete5000  | FastDB  | 179,938.7 |   326 KB |
 Delete10000 | FastDB  | 164,177.6 |   653 KB |
 **Delete1000**  | **LiteDB**  |   **1,337.3** |   **192 KB** |
 Delete5000  | LiteDB  |   1,256.5 |   848 KB |
 Delete10000 | LiteDB  |   1,207.7 | 1,664 KB |
 **Delete1000**  | **SQLite**  |     **741.9** |    **52 KB** |
 Delete5000  | SQLite  |     748.4 |   228 KB |
 Delete10000 | SQLite  |     757.6 |   444 KB |
 **Delete1000**  | **VistaDB** |   **5,182.7** |   **124 KB** |
 Delete5000  | VistaDB |   6,537.4 |   484 KB |
 Delete10000 | VistaDB |   5,503.5 |   940 KB |

## Upsert

Upserts 1K, 5K, and 10K records.

 Method      | Product | Op/s      | FileSize |
------------ |-------- |----------:|---------:|
 **Upsert1000**  | **FastDB**  | **102,105.1** |    **64 KB** |
 Upsert5000  | FastDB  |  98,313.4 |   326 KB |
 Upsert10000 | FastDB  |  93,633.9 |   653 KB |
 **Upsert1000**  | **LiteDB**  |   **3,471.8** |   **192 KB** |
 Upsert5000  | LiteDB  |   3,441.2 |   848 KB |
 Upsert10000 | LiteDB  |   3,192.2 | 1,664 KB |
 **Upsert1000**  | **SQLite**  |     **729.9** |    **52 KB** |
 Upsert5000  | SQLite  |     743.3 |   228 KB |
 Upsert10000 | SQLite  |     741.9 |   444 KB |
 **Upsert1000**  | **VistaDB** |   **4,935.4** |   **124 KB** |
 Upsert5000  | VistaDB |   3,442.2 |   484 KB |
 Upsert10000 | VistaDB |   2,372.8 |   940 KB |

## Bulk

Bulk inserts 1K, 5K, or 10K records. Almost all tested solutions demonstrate significant performance gains during bulk-insert operations. While VistaDB lacks a standard bulk insert API, it provides alternative methods for efficient data insertion, such as DDA (Direct Data Access) and CLR (Common Language Runtime) procedures.

 Method    | Product | Op/s      | FileSize |
---------- |-------- |----------:|---------:|
 **Bulk1000**  | **FastDB**  | **217,646.8** |    **64 KB** |
 Bulk5000  | FastDB  | 226,483.1 |   326 KB |
 Bulk10000 | FastDB  | 226,075.9 |   653 KB |
 **Bulk1000**  | **LiteDB**  |  **52,103.5** |     **8 KB** |
 Bulk5000  | LiteDB  |  44,871.5 |     8 KB |
 Bulk10000 | LiteDB  |  44,219.3 |     8 KB |
 **Bulk1000**  | **SQLite**  | **196,256.7** |    **52 KB** |
 Bulk5000  | SQLite  | 290,778.0 |   228 KB |
 Bulk10000 | SQLite  | 294,455.4 |   444 KB |
 **Bulk1000**  | **VistaDB** |   **2,582.1** |   **136 KB** |
 Bulk5000  | VistaDB |   4,657.8 |   496 KB |
 Bulk10000 | VistaDB |   2,706.4 |   952 KB |

 ## Query

Queries against a dataset of 1K, 5K, or 10K records and constructs a Customer instance for each result.

Queries records and returns a corresponding dataset.  
- FastDB and LiteDB: Customers.Where(a => a.Name.StartsWith("John") && a.Telephone > 5555555)
- SQLite and VistaDB: "SELECT * FROM Customers WHERE Name LIKE 'John%' AND Telephone > 5555555"
 
 Method     | Product | Op/s         | FileSize |
----------- |-------- |-------------:|---------:|
 **Query1000**  | **FastDB**  | **14,992,503.7** |    **64 KB** |
 Query5000  | FastDB  |  9,416,787.0 |   326 KB |
 Query10000 | FastDB  | 12,080,699.1 |   653 KB |
 **Query1000**  | **LiteDB**  |    **249,787.7** |   **136 KB** |
 Query5000  | LiteDB  |    446,071.4 |   816 KB |
 Query10000 | LiteDB  |    497,798.9 | 1,656 KB |
 **Query1000**  | **SQLite**  |  **2,200,704.2** |    **52 KB** |
 Query5000  | SQLite  |  1,553,824.5 |   228 KB |
 Query10000 | SQLite  |  2,227,601.5 |   444 KB |
 **Query1000**  | **VistaDB** |     **74,732.6** |   **124 KB** |
 Query5000  | VistaDB |    418,324.9 |   484 KB |
 Query10000 | VistaDB |    574,299.0 |   940 KB |



# Additional FastDB Benchmarks

The benchmarks presented here focus on FastDB's unique capabilities. FastDB effortlessly accomodates up to 1 million records through a combination of fast insertions and a compact storage footprint.

## FastDB Insert Performance

 Method        | Product | Mode   | Op/s      | FileSize |
-------------- |-------- |------- |----------:|---------:|
 **Insert1000**    | **FastDB**  | **Single** | **188,786.1** |    **36 KB** |
 Insert10000   | FastDB  | Single | 192,850.3 |   370 KB |
 Insert100000  | FastDB  | Single | 238,873.6 |  4084 KB |
 Insert1000000 | FastDB  | Single | 236,358.7 | 48479 KB |
 **Insert1000**    | **FastDB**  | **Bulk**   | **222,819.7** |    **36 KB** |
 Insert10000   | FastDB  | Bulk   | 231,856.1 |   370 KB |
 Insert100000  | FastDB  | Bulk   | 307,370.7 |  4084 KB |
 Insert1000000 | FastDB  | Bulk   | 294,974.6 | 48479 KB |


 ## FastDB Serialization and Parallelism

The default out-of-the-box configuration for FastDB does not include serialization contracts or parallel serialization. However, enabling parallelization significantly enhances the performance of serialization, compression, and encryption operations in FastDB. While the impact on small records is minimal, the difference becomes substantial when dealing with larger records, as demonstrated in the subsequent test.
 
 Method   | Product | Op/s      | FileSize |
--------- |-------- |----------:|---------:|
 Default 10000  | FastDB  | 196,703.5 |   653 KB |
 Contract 10000 | FastDB  | 194,597.2 |   370 KB |
 Parallel 10000 | FastDB  | 216,106.1 |   370 KB |

 ## FastDB Large Record Parallelism

When dealing with large records, which is often the case with serialized object graphs, the computational cost of compression and encryption can be substantial. In this benchmark, a significant amount of Lorem Ipsum text is stored both with and without encryption or compression. Enabling both compression and encryption markedly reduces throughput. However, by distributing this workload across threads, FastDB achieves nearly the same performance as writing unencrypted and uncompressed storage.

 Method                           | Product | Op/s      | FileSize |
--------------------------------- |-------- |----------:|---------:|
 Large                            | FastDB  | 124,763.3 | 20096 KB |
 LargeEncrypted                   | FastDB  |  95,370.7 | 20205 KB |
 LargeEncryptedCompressed         | FastDB  |  63,603.0 | 14892 KB |
 LargeEncryptedCompressedParallel | FastDB  | 124,720.8 | 14892 KB |

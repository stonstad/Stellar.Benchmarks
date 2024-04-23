using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public interface IBenchmark
    {
        void Setup(List<Customer> customers);
        void Cleanup();
        void Insert();
        void Bulk();
        void Delete();
        void Upsert();
        void Query();
        long GetFileSizeBytes();
    }
}

using Stellar.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stellar.Benchmarking
{
    public class FastDBTests : IBenchmark
    {
        private List<Customer> _TestData;
        private Random _Random = new Random(TestData.Seed0);

        // FastDB
        private FastDB _FastDB;
        private FastDBOptions _Options;
        private IFastDBCollection<int, Customer> _Customers;
        private string _FileName;

        public FastDBTests(FastDBOptions options)
        {
            _Options = options;
        }

        public void Setup(List<Customer> testData)
        {
            _TestData = testData;

            _FileName = Path.Combine(new FastDBOptions().DirectoryPath, "Customer.db");
            if (File.Exists(_FileName))
                File.Delete(_FileName);

            _FastDB = new FastDB(_Options);
            _Customers = _FastDB.GetCollection<int, Customer>();
        }

        public void Cleanup()
        {
            _FastDB.Close();
        }

        public void Insert()
        {
            foreach (Customer customer in _TestData)
                _Customers.Add(customer.Id, customer);
        }

        public void Bulk()
        {
            _Customers.AddBulkAsync(_TestData.ToDictionary(a => a.Id, a => a)).GetAwaiter().GetResult();
        }

        public void Delete()
        {
            foreach (Customer customer in _TestData)
                _Customers.Remove(customer.Id, out Customer removedCustomer);
        }

        public void Upsert()
        {
            foreach (Customer customer in _Customers)
            {
                customer.Telephone = _Random.Next(1000000, 9999999);
                _Customers.AddOrUpdate(customer.Id, customer);
            }
        }

        public void Query()
        {
            _Customers.Where(a => a.Name.StartsWith("John") && a.Telephone > 5555555).Count();
        }

        public long GetFileSizeBytes()
        {
            return new FileInfo(_FileName).Length;
        }
    }
}

using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stellar.Benchmarking
{
    public class LiteDBTests : IBenchmark
    {
        private List<Customer> _TestData;
        private Random _Random = new Random(TestData.Seed0);

        // LiteDB
        private LiteDatabase _LiteDatabase;
        private ILiteCollection<Customer> _Customers;
        private string _FileName;
        private string _ConnectionString;
        private bool _IsEncrypted;
        
        public LiteDBTests(bool isEncrypted = false)
        {
            _IsEncrypted = isEncrypted;
        }

        public void Setup(List<Customer> testData)
        {
            _TestData = testData;

            _FileName = Path.Combine(Environment.CurrentDirectory, "lite.db");
            if (_IsEncrypted)
                _ConnectionString = $"Filename={_FileName};Password=password";
            else
                _ConnectionString = $"Filename={_FileName};";

            if (File.Exists(_FileName))
                File.Delete(_FileName);

            _LiteDatabase = new LiteDatabase(_ConnectionString);
            _Customers = _LiteDatabase.GetCollection<Customer>();
        }

        public void Cleanup()
        {
            _LiteDatabase.Dispose();
        }

        public void Insert()
        {
            foreach (Customer customer in _TestData)
                _Customers.Insert(customer);
        }

        public void Bulk()
        {
            _Customers.InsertBulk(_TestData);
        }

        public void Delete()
        {
            foreach (Customer customer in _TestData)
                _Customers.Delete(customer.Id);
        }

        public void Upsert()
        {
            foreach (Customer customer in _TestData)
            {
                customer.Telephone = _Random.Next(1000000, 9999999);
                _Customers.Upsert(customer);
            }
        }

        public void Query()
        {
            _Customers.Find(a => a.Name.StartsWith("John") && a.Telephone > 5555555).Count();
        }

        public long GetFileSizeBytes()
        {
            if (_FileName != null)
                return new FileInfo(_FileName).Length;
            else
                return -1;
        }
    }
}

using Stellar.Collections;
using System;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public static class ProductFactory
    {
        /// <summary>
        /// The only product to benchmark, such as "FastDB", or null for all of them. Set it before running: BenchmarkDotNet
        /// reads the products in this process and passes each one to the child process that runs its benchmark.
        /// </summary>
        public static string OnlyProduct { get; set; }

        public static List<string> AvailableProducts
        {
            get
            {
                List<string> products = new List<string>()
                {
                    "FastDB",
                    "LiteDB",
                    "SQLite",
#if(VistaDB)
                    "VistaDB",
#endif
                };

                if (OnlyProduct != null)
                    return products.FindAll(a => string.Equals(a, OnlyProduct, StringComparison.OrdinalIgnoreCase));
                return products;
            }
        }

        public static IBenchmark Create(string product)
        {
            if (product == "FastDB")
                return new FastDBTests(new FastDBOptions());
            else if (product == "LiteDB")
                return new LiteDBTests();
            else if (product == "SQLite")
                return new SQLiteTests();
#if (VistaDB)
            else if (product == "VistaDB")
                return new VistaDBTests();
#endif
            else
                throw new ArgumentOutOfRangeException(product);
        }

    }
}

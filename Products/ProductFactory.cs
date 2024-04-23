using Stellar.Collections;
using System;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public static class ProductFactory
    {
        public static List<string> AvailableProducts = new List<string>()
        {
            "SQLite",
            "LiteDB",
#if(VistaDB)
            "VistaDB", 
#endif
            "FastDB"
        };

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

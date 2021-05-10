using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkLinqPerformance
{
    [MemoryDiagnoser]
    public class LinqTests
    {
        public List<Transaction> transactions = new List<Transaction>();
        public string[] domesticTransactions = new string[] { "abcd" };
        public string[] foreignTransactions = new string[] { "efgh" };

        [Params(1)]
        public int Gt { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            transactions.Add(new Transaction() { Name = "ABCD" });
            transactions.Add(new Transaction() { Name = "EFGH" });
            transactions.Add(new Transaction() { Name = "IJKM" });
            transactions.Add(new Transaction() { Name = "1234" });
            transactions.Add(new Transaction() { Name = "EFGH" });
            transactions.Add(new Transaction() { Name = "0000" });
        }

        [Benchmark]
        public List<Transaction> Linq_Filter_ToLower ()
        {
            var result = (from x in transactions
                          where
                            domesticTransactions.Contains(x.Name.ToLower()) ||
                            foreignTransactions.Contains(x.Name.ToLower())
                          select x).ToList();

            return result;
        }

        [Benchmark]
        public List<Transaction> Linq_Filter_ToLower_Let()
        {
            var result = (from x in transactions
                          let name = x.Name.ToLower()
                          where
                            domesticTransactions.Contains(name) ||
                            foreignTransactions.Contains(name)
                          select x).ToList();

            return result;
        }

        [Benchmark]
        public List<Transaction> Loop_Filter_ToLower()
        {
            List<Transaction> result = new List<Transaction>();

            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                var name = transaction.Name.ToLower();

                if (domesticTransactions.Contains(name) ||  foreignTransactions.Contains(name))
                {
                    result.Add(transaction);
                }
            }

            return result;
        }

        [Benchmark]
        public Transaction Linq_Last()
        {
            var result = transactions.Last();

            return result;
        }

        [Benchmark]
        public Transaction Array_Last()
        {
            var result = transactions[transactions.Count-1];

            return result;
        }

        [Benchmark]
        public List<string> Linq_GroupBy()
        {
            var transactionsByName = (from t in transactions
                                      group t by t.Name into names
                                      where names.Count() > Gt
                                      select names.Key);

            return transactionsByName.ToList();
        }

        [Benchmark]
        public List<string> Dictionary_GroupBy()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                if (dict.TryGetValue(transaction.Name, out var name))
                {
                    dict[transaction.Name]++;
                }
                else
                {
                    dict.Add(transaction.Name, 1);
                }
            }

            List<string> names = new List<string>();
            foreach (var kv in dict)
            {
                if (kv.Value > Gt)
                    names.Add(kv.Key);
            }

            return names;
        }
    }
}

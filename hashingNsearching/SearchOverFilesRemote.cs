using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashingNsearching
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchEngine
    {
        private Dictionary<string, List<string>> hashTable;

        public SearchEngine()
        {
            hashTable = new Dictionary<string, List<string>>();
        }

        public void IndexFiles(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                string fileContent = File.ReadAllText(filePath);
                string[] tokens = fileContent.Split(' ');

                foreach (string token in tokens)
                {
                    if (!hashTable.ContainsKey(token))
                    {
                        hashTable[token] = new List<string>();
                    }

                    hashTable[token].Add(filePath);
                }
            }
        }

        public void Search(string query, Action<string> callback)
        {
            string[] queryTokens = query.Split(' ');
            List<string> filePaths = new List<string>();

            foreach (string token in queryTokens)
            {
                if (hashTable.ContainsKey(token))
                {
                    filePaths.AddRange(hashTable[token]);
                }
            }

            Parallel.ForEach(filePaths, (filePath) =>
            {
                string fileContent = File.ReadAllText(filePath);
                if (fileContent.Contains(query))
                {
                    callback(filePath);
                }
            });
        }
    }
}

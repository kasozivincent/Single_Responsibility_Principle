using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain
{
    public class TradeProcessor
    {
        public void ProcessTrades(string filename)
        {
            //create a list to store the text in the file
            List<string> lines = new List<string>();

            //read from the file
            lines = File.ReadLines(filename).ToList();
        }
    }
}
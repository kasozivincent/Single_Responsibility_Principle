using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValueObjects;

namespace Domain
{
    public class TradeProcessor
    {
        public void ProcessTrades(string filename)
        {
            //create a list to store the text in the file
            List<string> unvalidatedTradeRecords = new List<string>();

            //read from the file
            unvalidatedTradeRecords = File.ReadLines(filename).ToList();

            int lineNumber = 1; //used for error handling
            List<TradeRecord> validatedTradeRecords = new List<TradeRecord>();

            foreach (string unvalidatedTradeRecord in unvalidatedTradeRecords)
            {
                //splitting along the comma (we assume that the fields are separated using commas)
                var recordFields = unvalidatedTradeRecord.Split(",").ToList();
                
                if(recordFields.Count != 6)
                {
                     Console.WriteLine($"Error on line {lineNumber}");
                     continue;
                }
                else
                {
                    var id = TransactionId.CreateId(recordFields[0]);
                    var clientName = ClientName.CreateName(recordFields[1]);
                    var itemName = ItemName.CreateItemName(recordFields[2]);
                   // var quantity = ItemQuantity.CreateItemQuantity(int.Parse(recordFields[3]));
                   // var unitPrice = Money.CreateMoney();
                   // var totalPrice = Money.CreateMoney();

                  //  var tradeRecord = new TradeRecord.CreateTradeRecord(id, clientName, itemName, quantity, unitPrice, totalPrice);

                }
            }
        }
    }
}
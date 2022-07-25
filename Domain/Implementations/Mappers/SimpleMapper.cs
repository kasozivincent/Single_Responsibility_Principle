using System;
using System.Collections.Generic;
using Domain;
using Domain.Services;
using ValueObjects;

namespace Domains.Implementations.Mappers
{
    public class SimpleMapper : IMapper
    {
        public TradeRecord Map(IList<string> fields)
        {
            var id = TransactionId.FromString(fields[0]);
            var clientName = ClientName.FromString(fields[1]);
            var itemName = ItemName.FromString(fields[2]);
            var quantity = ItemQuantity.FromInt(int.Parse(fields[3]));

            var currency = ParseMoney(fields[4], out Amount amount);
            var unitPrice = Money.CreateMoney(amount, currency);

            currency = ParseMoney(fields[5], out amount);
            var totalPrice = Money.CreateMoney(amount, currency);
            return TradeRecord.CreateTradeRecord(id, clientName, itemName, quantity, unitPrice, totalPrice);
        }

         private static Currency ParseMoney(string curr, out Amount amount){
            string currency = curr.Substring(0,3);
            string value = curr.Substring(3);

            amount = Amount.FromDecimal(decimal.Parse(value));
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
    }
}
using System;
using ValueObjects;

namespace Domain
{
    public class TradeRecord
    {
        public TransactionId Id { get; init;}
        public ClientName ClientName { get; init; }
        public ItemName ItemName { get; init; }
        public ItemQuantity ItemQuantity { get; init; }
        public Money UnitPrice { get; init; }
        public Money TotalPrice { get; set; }

        private TradeRecord(TransactionId id, ClientName clientName, 
                ItemName itemName, ItemQuantity qty, Money unitPrice, Money totalPrice)
        {
            this.Id = id;
            this.ClientName = clientName;
            this.ItemName = itemName;
            this.ItemQuantity = qty;
            this.UnitPrice = unitPrice;
            this.TotalPrice = totalPrice;
        }

        private TradeRecord CreateTradeRecord(TransactionId id, ClientName clientName, 
                ItemName itemName, ItemQuantity qty, Money unitPrice, Money totalPrice)
        {
            if(totalPrice >= unitPrice)
                return new TradeRecord(id, clientName, itemName, qty, unitPrice, totalPrice);
            else
                throw new Exception("Invalid TradeRecord");

        }
    }
}
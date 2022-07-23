namespace ValueObjects
{
    public class Money
    {
        public readonly Amount Amount;
        public readonly Currency Currency;

        private Money(Amount amount, Currency currency)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public static Money CreateMoney(Amount amount, Currency currency)
            =>  new Money(amount, currency);

        public static Money operator *(Money money, ItemQuantity qty)
            => new Money(money.Amount * qty.Qty, money.Currency);

        public static bool operator >= (Money a, Money b)
            => a.Currency == b.Currency ? a.Amount >= b.Amount : false;

        public static bool operator ==(Money a, Money b)
            =>  (a.Amount.Value == b.Amount.Value) && (a.Currency == b.Currency);

        public static bool operator !=(Money a, Money b)
            =>  (a.Currency == b.Currency) && (a.Amount.Value == b.Amount.Value);

        public static bool operator <= (Money a, Money b)
            => a.Currency == b.Currency ? a.Amount <= b.Amount : false;

        public override string ToString()
            => $"{Currency} {Amount}";

        
    }
}
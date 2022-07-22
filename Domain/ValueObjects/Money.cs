namespace ValueObjects
{
    public class Money
    {
        public readonly Amount Value;
        public readonly Currency Currency;

        private Money(Amount value, Currency currency)
        {
            this.Currency = currency;
            this.Value = value;
        }

        public Money CreateMoney(Amount value, Currency currency)
        {
            return new Money(value, currency);
        }

        public override string ToString()
            => $"{Value}, {Currency}";
    }
}
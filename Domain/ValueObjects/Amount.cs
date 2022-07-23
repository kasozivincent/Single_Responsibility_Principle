namespace ValueObjects
{
    public class Amount
    {
        public readonly decimal Value;

        private Amount(decimal value)
            => this.Value = value;

        public static Amount FromDecimal(decimal name)
            => ValidateAmount(name) ? new Amount(name) : throw new System.Exception("Invalid Amount");

        private static bool ValidateAmount(decimal value)
            => (value >= 1) && (value <= 2000000) ? true : false;

        public static Amount operator *(Amount amount, int a)
            => new Amount(amount.Value * a);
        public static bool operator >= (Amount a, Amount b)
            => a.Value >= b.Value;

         public static bool operator <= (Amount a, Amount b)
            => a.Value <= b.Value;

        public override string ToString()
            => $"{Value}";
    }

}

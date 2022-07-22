namespace ValueObjects
{
    public class Amount
    {
        public readonly decimal Value;

        private Amount(decimal value)
        {
            this.Value = value;
        }

        public static Amount CreateAmount(decimal name)
        {
            if(ValidateId(name))
                return new Amount(name);
            else
                throw new System.Exception("Invalid Amount");
        }

         private static bool ValidateId(decimal value)
        {
            if((value >= 1) && (value <= 2000000))
                return true;
            else
                return false;
        }

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

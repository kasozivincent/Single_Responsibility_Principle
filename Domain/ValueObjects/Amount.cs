namespace ValueObjects
{
    public class Amount
    {
        public readonly decimal Value;

        private Amount(decimal value)
        {
            this.Value = value;
        }

        public Amount CreateAmount(decimal name)
        {
            if(ValidateId(name))
                return new Amount(name);
            else
                throw new System.Exception("Invalid Amount");
        }

         private bool ValidateId(decimal value)
        {
            if((value >= 1) && (value <= 2000000))
                return true;
            else
                return false;
        }

        public static bool operator >= (Amount a, Amount b)
            => a.Value >= b.Value;

         public static bool operator <= (Amount a, Amount b)
            => a.Value <= b.Value;

        public override string ToString()
            => $"{Value}";
    }

}

using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class ItemQuantity
    {
        public readonly int Qty;

        private ItemQuantity(int qty)
        {
            this.Qty = qty;
        }

        public static ItemQuantity CreateItemQuantity(int qty)
        {
            if(ValidateId(qty))
                return new ItemQuantity(qty);
            else
                throw new System.Exception("Invalid name");
        }

        private static bool ValidateId(int qty)
        {
            if((qty >= 1) && (qty <= 20))
                return true;
            else
                return false;
        }

        public override string ToString()
            => $"{Qty}";
    }
} 

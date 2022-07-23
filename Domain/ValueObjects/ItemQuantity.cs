using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class ItemQuantity
    {
        public readonly int Qty;

        private ItemQuantity(int qty)
           => this.Qty = qty;

        public static ItemQuantity FromInt(int qty)
           => ValidateId(qty) ? new ItemQuantity(qty) : throw new System.Exception("Invalid name");

        private static bool ValidateId(int qty)
            => (qty >= 1) && (qty <= 20) ? true : false;

        public override string ToString()
            => $"{Qty}";
    }
} 

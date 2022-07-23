using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class TransactionId
    {
        private string Id;

        private TransactionId(string Id)
            => this.Id = Id;

        public static TransactionId FromString(string Id)
            => ValidateId(Id) ? new TransactionId(Id) : throw new System.Exception("Invalid Id");

        private static bool ValidateId(string Id)
            => new Regex("^[A-Z]+[0-9]+$").IsMatch(Id);

        public override string ToString()
            => $"{Id}";
    }
}
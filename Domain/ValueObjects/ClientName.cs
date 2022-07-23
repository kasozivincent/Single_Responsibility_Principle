using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class ClientName
    {
        public readonly string Name;

        private ClientName(string name)
            => this.Name = name;

        public static ClientName FromString(string name)
            => ValidateClientName(name) ? new ClientName(name) : throw new System.Exception("Invalid name");

        private static bool ValidateClientName(string name)
            => new Regex("[A-Za-z]+").IsMatch(name);

        public override string ToString()
            => $"{Name}";
    }
}
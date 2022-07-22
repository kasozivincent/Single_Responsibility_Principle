using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class ClientName
    {
        public readonly string Name;

        private ClientName(string name)
        {
            this.Name = name;
        }

        public ClientName CreateId(string name)
        {
            if(ValidateId(name))
                return new ClientName(name);
            else
                throw new System.Exception("Invalid name");
        }

        private bool ValidateId(string name)
        {
            Regex regex = new Regex("[A-Za-z]+");
            if(name.Length <= 20)
                return regex.IsMatch(name);
            else
                return false;
        }

        public override string ToString()
            => $"{Name}";
    }
}
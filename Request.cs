using System.Collections.Generic;

namespace FileSystemApp
{
    public class Request
    {
        public string Action { get; }
        public List<string> Parameters { get; } = new List<string>();

        public Request(string input)
        {
            var inputs = input.Split(' ');
            this.Action = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                Parameters.Add(inputs[i]);
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Action) || this.Parameters.Count == 0)
            {
                return false;
            }

            switch (this.Action)
            {
                case "lend":
                    return this.Parameters.Count == 2 && !string.IsNullOrEmpty(this.Parameters[0]) && int.TryParse(this.Parameters[1], out _);
                case "receive":
                    return this.Parameters.Count == 2 && !string.IsNullOrEmpty(this.Parameters[0]) && int.TryParse(this.Parameters[1], out _);
                case "list":
                    return this.Parameters.Count == 0;
                default:
                    return false;
            }
        }
    }
}
using System.Collections.Generic;

namespace FileSystemApp
{
    public class RequestProcessor
    {
        private Dictionary<string, int> debt = new Dictionary<string, int>();
        private int MAX_AMOUNT = 5000;

        public string ProcessRequest(Request request)
        {
            if (!request.IsValid())
            {
                return "Invalid request";
            }

            List<string> parameters = request.Parameters;
            string command = parameters[0];

            switch (command)
            {
                case "lend":
                    return Lend(parameters);
                case "receive":
                    return Receive(parameters);
                case "list":
                    return ListDebtors();
                default:
                    return "Unknown command";
            }
        }

        private string Lend(List<string> parameters)
        {
            string name = parameters[1];
            int amount = int.Parse(parameters[2]);

            if (!debt.ContainsKey(name))
            {
                debt.Add(name, amount);
            }
            else
            {
                debt[name] += amount;
            }

            if (debt[name] > MAX_AMOUNT)
            {
                debt[name] = MAX_AMOUNT;
            }

            return $"{name} {debt[name]}";
        }

        private string Receive(List<string> parameters)
        {
            string name = parameters[1];
            int amount = int.Parse(parameters[2]);

            if (!debt.ContainsKey(name))
            {
                return "0";
            }

            int remainingDebt = debt[name] - amount;

            if (remainingDebt < 0)
            {
                return $"{debt[name]}";
            }
            else if (remainingDebt == 0)
            {
                debt.Remove(name);
                return "0";
            }
            else
            {
                debt[name] = remainingDebt;
                return $"{remainingDebt}";
            }
        }

        private string ListDebtors()
        {
            if (debt.Count == 0)
            {
                return "No debtors";
            }

            var sortedDebtors = debt.OrderByDescending(d => d.Value);

            int i = 1;
            string result = "";

            foreach (var debtor in sortedDebtors)
            {
                result += $"{i}. {debtor.Key} ({debtor.Value})\n";
                i++;
            }

            return result.TrimEnd();
        }
    }
}

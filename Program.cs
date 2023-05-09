using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileSystemApp
{

    
    class Program
    {
        public static void PerformOperation(List<Request> requests,string[] lines,List<string> results)
        {
        foreach(string line in lines)
    {
        Request request = new Request(line);

        switch (request.Action)
        {
            case "lend":
                int amount = int.Parse(request.Parameters[1]);
                if (amount <= 0)
                {
                    results.Add("Invalid amount. Please try again.");
                    continue;
                }
                requests.Add(request);
                results.Add($"{request.Parameters[0]} {amount}");
                break;
            case "receive":
                amount = int.Parse(request.Parameters[1]);
                if (amount <= 0)
                {
                    results.Add("Invalid amount. Please try again.");
                    continue;
                }
                bool userFound = false;
                foreach (var req in requests)
                {
                    if (req.Parameters[0] == request.Parameters[0])
                    {
                        userFound = true;
                        int currAmount = int.Parse(req.Parameters[1]);
                        int remainder = currAmount - amount;
                        if (remainder < 0)
                        {
                            results.Add("Insufficient funds. Please try again.");
                            continue;
                        }
                        else
                        {
                            req.Parameters[1] = remainder.ToString();
                            results.Add($"Change: {remainder}");
                            break;
                        }
                    }
                }
                if (!userFound)
                {
                    results.Add("User not found.");
                    continue;
                }
                break;
            case "list":
                if (requests.Count == 0)
                {
                    results.Add("No debtors found.");
                    break;
                }
                results.Add("Debtors:");
                var sortedRequests = requests.OrderBy(r => -int.Parse(r.Parameters[1]));
                for (int j = 0; j < sortedRequests.Count(); j++)
                {
                    results.Add($"{j + 1}. {sortedRequests.ElementAt(j).Parameters[0]} ({sortedRequests.ElementAt(j).Parameters[1]})");
                }
                break;



            default:
                results.Add("Invalid command. Please try again. ^-^");
                break;
        }
       

    }       
            
           
            
                Console.WriteLine("Writing results to file...");
                File.WriteAllLines("/sync/results.txt", results);
           
        }
       static void Main(string[] args)
    {
        while(true)
        {
            string requestTxt = ("/sync/request.txt");
            List<Request> requests = new List<Request>();
            List<string> results = new List<string>();
            string[] lines = File.ReadAllLines(requestTxt); newStep: 
            if (File.Exists(requestTxt))
            {
                PerformOperation(requests,lines,results);
                File.Delete("/sync/request.txt");
            }
            else 
            {
            Console.WriteLine($"Error: {requestTxt} file not found.");
            Thread.Sleep(5000);
            goto newStep;
            }
        
     
     
        }
        }
    }


    
    
}
    
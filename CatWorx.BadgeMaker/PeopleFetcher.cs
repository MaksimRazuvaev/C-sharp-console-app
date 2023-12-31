using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace  CatWorx.BadgeMaker 
{
  class PeopleFetcher 
  {
    public static List<Employee> GetEmployees()
    {
      // I will return a List of strings
      List<Employee> employees = new List<Employee>();
      while (true) 
      {
        Console.WriteLine("Please enter first name: (leave empty to exit): ");
        string firstName = Console.ReadLine() ?? "";
        if (firstName == "") 
        {
          break;
        }
        
        // Create a new Employee instance
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine() ?? "";
        Console.Write("Enter ID: ");
        int id = Int32.Parse(Console.ReadLine() ?? "");
        Console.Write("Enter Photo URL:");
        string photoUrl = Console.ReadLine() ?? "";
        Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
        employees.Add(currentEmployee);
      }
      // This is important!
      return employees;
    }

    async public static Task<List<Employee>> GetFromApi()
    {
      List<Employee> employees = new List<Employee>();

      using (HttpClient client = new HttpClient())
      {
        string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");

        // Console.WriteLine(response);
        JObject json = JObject.Parse(response);

        foreach(JToken token in json.SelectToken("results")!)
        {
          // Parse JSON data
          Employee emp = new Employee
          (
              token.SelectToken("name.first")!.ToString(),
              token.SelectToken("name.last")!.ToString(),
              Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
              token.SelectToken("picture.large")!.ToString()
          );
          employees.Add(emp);
        }
      }
      return employees;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    async static Task Main(string[] args)
    {

      List<Employee> employees = new List<Employee>();

      Console.WriteLine("Welcome to the CatWorx BadgeMaker Program!");
      Console.WriteLine("Do you want enter employees manually? 'Y' or 'N'");
      string answer = Console.ReadLine() ?? "";
      if (answer == "y" || answer == "Y")
      {
        employees = PeopleFetcher.GetEmployees();
      }
      else
      {
        Console.WriteLine("Here are random employees from API");
        employees = await PeopleFetcher.GetFromApi();
      }

      Util.PrintEmployees(employees);
      Util.MakeCSV(employees);
      await Util.MakeBadges(employees);
    }
  }
}
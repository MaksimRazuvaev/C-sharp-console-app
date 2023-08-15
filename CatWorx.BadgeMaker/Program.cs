﻿using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Employee> employees = GetEmployees();
      Util.PrintEmployees(employees);
    }
    
    static List<Employee> GetEmployees()
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
  }
}
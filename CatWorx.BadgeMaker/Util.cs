using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;


namespace CatWorx.BadgeMaker
{
  class Util
  {
    // Method declared as "static"
    public static void PrintEmployees(List<Employee> employees)
    {
      for (int i = 0; i < employees.Count; i++) 
      {
        string template = "{0,-10}\t{1,-20}\t{2}";
        Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
      }
    }

    //Method to save data to CSV file
    public static void MakeCSV(List<Employee> employees)
    {
      // Check to see if folder exists
      if (!Directory.Exists("data")) 
      {
        // If not, create it
        Directory.CreateDirectory("data");
      }
      using (StreamWriter file = new StreamWriter("data/employees.csv"))
      {
          // Loop over employees
        for (int i = 0; i < employees.Count; i++)
        {
          // Write each employee to the file
          string template = "{0},{1},{2}";
          file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
        }
      }
    }

    public static void MakeBadges(List<Employee> employees)
    {
      // Create image
      SKImage newImage = SKImage.FromEncodedData(File.OpenRead("badge.png"));
      SKData data = newImage.Encode();
      data.SaveTo(File.OpenWrite("data/employeeBadge.png"));
    }
  }
}
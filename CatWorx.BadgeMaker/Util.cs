using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;
using System.Threading.Tasks;


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

    async public static Task MakeBadges(List<Employee> employees) {

      // Layout variables
      int BADGE_WIDTH = 669;
      int BADGE_HEIGHT = 1044;

      int PHOTO_LEFT_X = 184;
      int PHOTO_TOP_Y = 215;
      int PHOTO_RIGHT_X = 486;
      int PHOTO_BOTTOM_Y = 517;
    

      using(HttpClient client = new HttpClient())
      {
        for (int i = 0; i < employees.Count; i++)
        {
          SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));
          SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

          SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);
          SKCanvas canvas = new SKCanvas(badge);

          canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
          canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));
          
          SKImage finalImage = SKImage.FromBitmap(badge);
          SKData data = finalImage.Encode();
          data.SaveTo(File.OpenWrite("data/employeeBadge.png"));
        }
      }
    }
  }
}
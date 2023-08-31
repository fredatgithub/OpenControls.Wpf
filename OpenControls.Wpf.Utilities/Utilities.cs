using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace OpenControls.Wpf.Utilities
{
  public static class Utilities
  {
    public static bool Parse(string text, out CornerRadius cornerRadius)
    {
      cornerRadius = new CornerRadius();

      Match match = Regex.Match(text, @"(\d),(\d),(\d),(\d)");
      if (!match.Success)
      {
        return false;
      }

      cornerRadius = new System.Windows.CornerRadius(
          Convert.ToDouble(match.Groups[1].Value),
              Convert.ToDouble(match.Groups[2].Value),
              Convert.ToDouble(match.Groups[3].Value),
              Convert.ToDouble(match.Groups[4].Value)
              );

      return true;
    }

    public static bool Parse(string text, out Thickness thickness)
    {
      thickness = new Thickness();

      Match match = Regex.Match(text, @"(\d),(\d),(\d),(\d)");
      if (!match.Success)
      {
        return false;
      }

      thickness = new System.Windows.Thickness(
          Convert.ToDouble(match.Groups[1].Value),
              Convert.ToDouble(match.Groups[2].Value),
              Convert.ToDouble(match.Groups[3].Value),
              Convert.ToDouble(match.Groups[4].Value)
              );

      return true;
    }

    public static string GetAppDataFolder()
    {
      string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      path = System.IO.Path.Combine(path, AppDomain.CurrentDomain.FriendlyName);
      if (!System.IO.Directory.Exists(path))
      {
        System.IO.Directory.CreateDirectory(path);
      }

      return path;
    }
  }
}

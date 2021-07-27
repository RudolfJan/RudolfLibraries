using System;
using System.Reflection;

namespace Utilities.Library.Wpf.Models
  {
  public interface IAboutModel
    {
    string AboutImagePath { get; set; }
    string AboutWindowText { get; }
    string Company { get; }
    string Copyright { get; }
    Assembly CurrentAssembly { get; set; }
    string Description { get; }
    Uri DownloadLocation { get; set; }
    string DownloadUri { get; set; }
    string Product { get; }
    string Version { get; set; }
    }
  }
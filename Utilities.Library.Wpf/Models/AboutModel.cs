using System;
using System.Reflection;

namespace Utilities.Library.Wpf.Models
	{

  public class AboutModel : IAboutModel
    {


    public Assembly CurrentAssembly { get; set; } = null;
    private T GetAssemblyAttribute<T>() where T : Attribute
      {
      if (CurrentAssembly != null)
        {
        Object[] Attributes = CurrentAssembly
          .GetCustomAttributes(typeof(T), true);
        if (Attributes.Length == 0) return null;
        return (T)Attributes[0];
        }
      else
        {
        return null;
        }
      }

    public string Company
      {
      get
        {
        var Attr = GetAssemblyAttribute<AssemblyCompanyAttribute>();
        if (Attr != null)
          return Attr.Company;
        return string.Empty;
        }
      }

    public string Copyright
      {
      get
        {
        var Attr = GetAssemblyAttribute<AssemblyCopyrightAttribute>();
        if (Attr != null)
          return Attr.Copyright;
        return String.Empty;
        }
      }

    public string Product
      {
      get
        {
        var Attr = GetAssemblyAttribute<AssemblyProductAttribute>();
        if (Attr != null)
          return Attr.Product;
        return String.Empty;
        }
      }

    public string AboutWindowText
      {
      get
        {
        return $"About {Product}";
        }
      }
    public string Description
      {
      get
        {
        var Attr = GetAssemblyAttribute<AssemblyDescriptionAttribute>();
        if (Attr != null)
          return Attr.Description;
        return string.Empty;
        }
      }

    public string DownloadUri { get; set; }

    public string Version { get; set; }

    public Uri DownloadLocation { get; set; }
    public string AboutImagePath { get; set; }
    }
  }

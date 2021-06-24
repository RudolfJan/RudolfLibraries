using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utilities.About;

namespace Utilities.Library.Wpf.ViewModels
  {
  public class AboutViewModel: Notifier
    {
    private AboutModel _AboutData= new AboutModel();
    public AboutModel AboutData
      {
      get { return _AboutData; }
      set
        {
        _AboutData = value;
        OnPropertyChanged("AboutData");
        }
      }

    public AboutViewModel(Assembly currentAssembly, string version, string imagePath, string downlLoadUrl)
      {
     
      AboutData= new AboutModel();
      AboutData.CurrentAssembly= currentAssembly;
      AboutData.Version= version;
      AboutData.DownloadUri= downlLoadUrl;
      AboutData.AboutImagePath = imagePath;
      }
    }
  }

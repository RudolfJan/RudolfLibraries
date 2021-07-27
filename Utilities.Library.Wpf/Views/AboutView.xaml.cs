using System;
using System.Diagnostics;
using System.Windows;
using Utilities.Library.Wpf.ViewModels;

namespace Utilities.Library.Wpf.Views
{
  
    public partial class AboutView
    {
     public AboutView()
        {
            InitializeComponent();
  		    }

	    private void Hyperlink_RequestNavigate(Object Sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		    {
      // You need a workaround here for .Net Core:
      //  https://github.com/dotnet/runtime/issues/28005
      var psi = new ProcessStartInfo
        {
        FileName = e.Uri.AbsoluteUri,
        UseShellExecute = true
        };
      Process.Start(psi);
      }

 
    }
}

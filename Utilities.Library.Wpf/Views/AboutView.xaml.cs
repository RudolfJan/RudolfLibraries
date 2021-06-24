using System;
using System.Windows;
using Utilities.Library.Wpf.ViewModels;

namespace Utilities.Library.Wpf.Views
{
  
    public partial class AboutView
    {
	    public AboutViewModel About { get; set; }
        public AboutView(AboutViewModel about)
        {
            InitializeComponent();
            About= about;
		        DataContext = About;
		    }

	    private void Hyperlink_RequestNavigate(Object Sender, System.Windows.Navigation.RequestNavigateEventArgs E)
		    {
		    System.Diagnostics.Process.Start(About.AboutData.DownloadUri);
		    }

        private void OnOkButtonClicked(Object Sender, RoutedEventArgs E)
        {
						Close();
        }
    }
}

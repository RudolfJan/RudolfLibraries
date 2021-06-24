using Filter.Library.WPF.ViewModels;
using System.Windows;
using Utilities.Library.Wpf.ViewModels;
using Utilities.Library.Wpf.Views;

namespace WPFDemo
  {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
    {
    public TagAndCategoryViewModel TagAndCategoryManager { get;set;}
  
    public MainWindow()
      {
      InitializeComponent();

      }

    private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
      {
      var form= new ScreenshotDemoView();
      form.Show();
      }

    private void TreeViewButton_Click(object sender, RoutedEventArgs e)
      {
      var form= new FileTreeDemoView();
      form.Show();
      }

    private void AboutButton_Click(object sender, RoutedEventArgs e)
      {
      // https://social.msdn.microsoft.com/Forums/en-US/7f58c338-6ffc-4ee8-943e-ef7f70f97111/wpf-about-box?forum=wpf

      var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      AboutViewModel about= new AboutViewModel(currentAssembly,"0.1 alpha","../../Images/about.jpg","https://www.hollandhiking.nl/trainsimulator");
      var form = new AboutView(about);
      form.Show();
      }
    }
  }

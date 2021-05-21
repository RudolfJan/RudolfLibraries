using Filter.Library.WPF.ViewModels;
using Screenshots.Library.WPF.ViewModels;
using Screenshots.Library.WPF.Views;
using SQLiteDatabase.Library;
using System.Windows;

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
    }
  }

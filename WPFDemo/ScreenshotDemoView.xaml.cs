using Filter.Library.WPF.ViewModels;
using System.Windows;

namespace WPFDemo
{
  /// <summary>
  /// Interaction logic for ScreenshotDemoView.xaml
  /// </summary>
  public partial class ScreenshotDemoView : Window
  {
    public TagAndCategoryViewModel TagAndCategoryManager { get; set; }

    public ScreenshotDemoView()
    {
      //InitializeComponent();
      //string databasePath = "ScreenshotsDemo.db"; // will run in application folder
      //string connectionString = $"Data Source = {databasePath}; Version = 3;";
      //var factory = new Screenshots.Library.DataAccess.DatabaseFactory();
      //DbManager.InitDatabase(connectionString, databasePath, factory);
      //TagAndCategoryManager = new TagAndCategoryViewModel();
      //ScreenshotManagerViewModel screenshotManager = new ScreenshotManagerViewModel();
      //ScreenshotManagerView.DataContext = screenshotManager;
      //ScreenshotManagerView.ScreenshotManager = screenshotManager;
      //DataContext = TagAndCategoryManager;
      //TagMaintenanceView.DataContext = TagAndCategoryManager;
      //TagMaintenanceView.TagAndCategoryData = TagAndCategoryManager;
      //ScreenshotCollectionViewModel CollectionManager = new ScreenshotCollectionViewModel();
      //CollectionMaintenanceView.DataContext = CollectionManager;
      //CollectionMaintenanceView.ScreenshotCollectionManager = CollectionManager;
    }
  }
}

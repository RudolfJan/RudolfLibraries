﻿using Filter.Library.WPF.ViewModels;
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
      string databasePath = "ScreenshotsDemo.db"; // will run in application folder
      string connectionString = $"Data Source = {databasePath}; Version = 3;";
      var factory = new Screenshots.Library.DataAccess.DatabaseFactory();
      DbManager.InitDatabase(connectionString, databasePath, factory);
      TagAndCategoryManager = new TagAndCategoryViewModel();
      DataContext= TagAndCategoryManager;
      TagMaintenanceView.DataContext = TagAndCategoryManager;
      TagMaintenanceView.TagAndCategoryData= TagAndCategoryManager;
      }
    }
  }

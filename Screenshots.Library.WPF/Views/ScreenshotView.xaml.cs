using Screenshots.Library.Models;
using Screenshots.Library.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Screenshots.Library.WPF.Views
  {
  /// <summary>
  /// Interaction logic for ScreenshotView.xaml
  /// </summary>
  public partial class ScreenshotView : Window
    {
    public ScreenshotViewModel Screenshot { get; set; }
   
    public ScreenshotView()
      {
      InitializeComponent();
      
      }

    private void OnSaveAsButtonClicked(object sender, RoutedEventArgs e)
      {
      Screenshot.SaveScreenshotToDisk();
      }

    private void OnDeleteButtonClicked(object sender, RoutedEventArgs e)
      {
      Screenshot.DeleteScreenshot();
      Close();
      }

    private void OnOkButtonClicked(object sender, RoutedEventArgs e)
      {
      Close();
      }

   private void OnChooseTagTextChanged(object sender, TextChangedEventArgs e)
     {
     Screenshot.TagPattern= ChooseTag.Text; Screenshot.UpdateTagFilter();
     FilteredTagsDataGrid.Items.Refresh();
     }

    private void OnFilteredTagListSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      if (Screenshot.SelectedTag != null)
        {
        Screenshot.AddTag();
        ImageTagsItemControl.Items.Refresh();
        }
      }

    private void OnImageTagsSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      if (Screenshot.SelectedImageTag != null)
        {
        Screenshot.RemoveTag();
        ImageTagsItemControl.Items.Refresh();
        }
      }

    private void OnShowImageFullScreenClicked(object sender, RoutedEventArgs e)
      {
      var form = new FullScreenScreenshotView
        {
        ImagePath = Screenshot.ImagePath,
        DataContext= Screenshot.ImagePath
        };
      form.Show();
      }
    }
  }

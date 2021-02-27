using Screenshots.Library.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Screenshots.Library.WPF.Views
  {

  public partial class ScreenshotCollectionMaintenanceView : UserControl
    {
    public ScreenshotCollectionViewModel ScreenshotCollectionManager { get; set; }

    public ScreenshotCollectionMaintenanceView()
      {
      InitializeComponent();
      }

    private void SetControlStates()
      {
      EditCollectionButton.IsEnabled= ScreenshotCollectionManager?.SelectedCollection!=null;
      DeleteCollectionButton.IsEnabled= ScreenshotCollectionManager?.SelectedCollection != null;
      CollectionFolderFileInput.IsEnabled = ScreenshotCollectionManager?.CollectionId == 0;
      }



    private void EditCollectionClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotCollectionManager.EditCollection();
      SetControlStates();
      }

    private void DeleteCollectionClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotCollectionManager.DeleteCollection();
      ScreenshotCollectionDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnSelectedCollectionChanged(object sender, SelectionChangedEventArgs e)
      {
      SetControlStates();
      }

    private void SaveCollectionClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotCollectionManager.SaveCollection();
      ScreenshotCollectionDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void ClearCollectionClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotCollectionManager.ClearCollection();
      SetControlStates();
      }

    private void OnControlLoaded(object sender, RoutedEventArgs e)
      {
      SetControlStates();
      }
    }
  }

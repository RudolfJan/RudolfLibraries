using Screenshots.Library.Models;
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
  /// <summary>
  /// Interaction logic for ScreenshotManagerView.xaml
  /// </summary>
  public partial class ScreenshotManagerView : UserControl
    {
    public ScreenshotManagerViewModel ScreenshotManager { get; set; }
    public ScreenshotManagerView()
      {
      InitializeComponent();
      var WinHeight = SystemParameters.MaximizedPrimaryScreenHeight * 0.85;
      var AllowedHeightFactor = 0.8;
      if (ThumbNailScrollView.Height / WinHeight > AllowedHeightFactor)
        {
        ThumbNailScrollView.Height = ThumbNailScrollView.MaxHeight *
                                     ThumbNailScrollView.MaxHeight / WinHeight;

        }
      
      SetControlStates();
      }

    private void SetControlStates()
      {
      if (ScreenshotManager != null)
        {
        NextButton.IsEnabled = ScreenshotManager.PageStartIndex <
                               ScreenshotManager.TotalScreenshotCount-ScreenshotManagerViewModel.ScreenshotsPerPage;
        PreviousButton.IsEnabled = ScreenshotManager.PageStartIndex > 0 &&
                                   ScreenshotManager.TotalScreenshotCount >
                                   ScreenshotManagerViewModel.ScreenshotsPerPage;
        LastButton.IsEnabled = NextButton.IsEnabled;
        FirstButton.IsEnabled = PreviousButton.IsEnabled;
        CategoryFilterComboBox.IsEnabled= ScreenshotManager.SelectUntaggedScreenshots==false;
        }
      }

    private void OnPreviousButtonClicked(object Sender, RoutedEventArgs E)
      {
      ScreenshotManager.DecreaseDisplayPage();
      ScreenshotsItemsControl.Items.Refresh();
      PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
      SetControlStates();
      }

    private void OnNextButtonClicked(object Sender, RoutedEventArgs E)
      {
      ScreenshotManager.IncreaseDisplayPage();
      ScreenshotsItemsControl.Items.Refresh();
      PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
      SetControlStates();
      }

    private void OnFirstButtonClicked(object Sender, RoutedEventArgs E)
      {
      ScreenshotManager.GetFirstPage();
      ScreenshotsItemsControl.Items.Refresh();
      PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
      SetControlStates();
      }

    private void OnLastButtonClicked(object Sender, RoutedEventArgs E)
      {
      ScreenshotManager.GetLastPage();
      ScreenshotsItemsControl.Items.Refresh();
      PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
      SetControlStates();
      }

    private void OnThumbNailButtonClicked(object sender, RoutedEventArgs e)
      {
      // Go down starting at the button to retrieve the Image path

      var Screenshot = ((ThumbNailButton)sender).Screenshot;
      if (Screenshot != null)
        {
        var Form = new ScreenshotView();
        var tmp = new ScreenshotViewModel(Screenshot);
        Form.Screenshot = tmp;
        Form.DataContext = tmp;
        Form.ShowDialog();

        // TODo performance improvement, check if anything was changed that is relevant.
        // Example is screenshot is deleted, or tags are added or all tags are removed
        ScreenshotManager.FilteredScreenshotList = ScreenshotManager.GetFilteredScreenshotList();
        ScreenshotManager.GetFirstPage();
        NrSelectedTextBox.TextBoxText = ScreenshotManager.TotalFilteredScreenshotCount.ToString();
        PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
        ScreenshotsItemsControl.Items.Refresh();

        }

      SetControlStates();
      }

    private void OnScreenshotManagerViewLoaded(object sender, RoutedEventArgs e)
      {
      SetControlStates();
      }

    private void RefreshThumbnailGrid()
      {
      ScreenshotManager.FilteredScreenshotList = ScreenshotManager.GetFilteredScreenshotList();
      ScreenshotManager.GetFirstPage();
      ScreenshotsItemsControl.Items.Refresh();
      NrSelectedTextBox.TextBoxText = ScreenshotManager.TotalFilteredScreenshotCount.ToString();
      PageTextBox.TextBoxText = ScreenshotManager.FormattedPage;
      SetControlStates();
      }

    private void CheckedUntaggedScreenshotsCheckbox(object sender, RoutedEventArgs e)
      {
      if (ScreenshotManager.SelectUntaggedScreenshots)
        {
        ScreenshotManager.SelectedCategory=null;
        ScreenshotManager.SelectedTag=null;
        ScreenshotManager.UpdateTagFilter();
        ChooseTag.Text = string.Empty;
        TagFilterComboBox.IsDropDownOpen = false;
        }
      RefreshThumbnailGrid();
      }

    private void OnCollectionChanged(object sender, SelectionChangedEventArgs e)
      {
      RefreshThumbnailGrid();
      }

    private void OnResetCollectionFilterClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotManager.SelectedCollection=null;
      RefreshThumbnailGrid();
      }

    private void OnResetCategoryFilterClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotManager.SelectedCategory = null;
      ScreenshotManager.UpdateTagFilter();
      TagFilterComboBox.Items.Refresh();
      ScreenshotsItemsControl.Items.Refresh();
      RefreshThumbnailGrid();
      }

    private void OnCategoryChanged(object sender, SelectionChangedEventArgs e)
      {
      RefreshThumbnailGrid();
      }

    private void OnChooseTagTextChanged(object sender, TextChangedEventArgs e)
      { 
      ScreenshotManager.UpdateTagFilter();
      TagFilterComboBox.IsDropDownOpen=true;
      TagFilterComboBox.Items.Refresh();
      ChooseTag.Focus();
      }

    private void SelectedTagChanged(object sender, SelectionChangedEventArgs e)
      {
      TagFilterComboBox.IsDropDownOpen = false;
      RefreshThumbnailGrid();
      }

    private void OnResetTagFilterClicked(object sender, RoutedEventArgs e)
      {
      ScreenshotManager.SelectedTag = null;
      ScreenshotManager.UpdateTagFilter();
      ChooseTag.Text=string.Empty;
      TagFilterComboBox.IsDropDownOpen = false;
      TagFilterComboBox.Items.Refresh();
      RefreshThumbnailGrid();
      }
    }
  }
